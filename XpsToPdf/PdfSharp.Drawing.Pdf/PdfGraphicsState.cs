#region PDFsharp - A .NET library for processing PDF
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//
// Copyright (c) 2005-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
#if GDI
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
#if WPF
#endif
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Internal;

namespace PdfSharp.Drawing.Pdf
{
  // TODO: update the following text
  //
  // In PDF the current transformation matrix (CTM) can only be modified, but not set. The XGraphics
  // object allows to set the transformation matrix, which leads to a problem. In PDF the only way
  // to reset the CTM to its original value is to save and restore the PDF graphics state. Don't try
  // to keep track of every modification and then reset the CTM by multiplying with the inverse matrix
  // of the product of all modifications. PDFlib uses this 'trick', but it does not work. Because of
  // rounding errors everything on the PDF page looks sloping after some resets. Saving and restoring
  // the graphics state is the only possible way to reset the CTM, but because the PDF restore operator
  // 'Q' resets not only the CTM but all other graphics state values, we have to implement our own 
  // graphics state management. This is apparently the only safe way to give the XGrahics users the 
  // illusion that they can arbitrarily set the transformation matrix.
  // 
  // The current implementation is just a draft. Save/Restore works only once and clipping is not
  // correctly restored in some cases.

  /// <summary>
  /// Represents the current PDF graphics state.
  /// </summary>
  internal sealed class PdfGraphicsState : ICloneable
  {
    public PdfGraphicsState(XGraphicsPdfRenderer renderer)
    {
      this.renderer = renderer;
    }
    readonly XGraphicsPdfRenderer renderer;

    public PdfGraphicsState Clone()
    {
      PdfGraphicsState state = (PdfGraphicsState)MemberwiseClone();
      return state;
    }

    object ICloneable.Clone()
    {
      return Clone();
    }

    internal int Level;

    internal InternalGraphicsState InternalState;

    public void PushState()
    {
      //BeginGraphic
      renderer.Append("q/n");
    }

    public void PopState()
    {
      //BeginGraphic
      renderer.Append("Q/n");
    }

    #region Stroke

    double realizedLineWith = -1;
    int realizedLineCap = -1;
    int realizedLineJoin = -1;
    double realizedMiterLimit = -1;
    XDashStyle realizedDashStyle = (XDashStyle)(-1);
    string realizedDashPattern;
    XColor realizedStrokeColor = XColor.Empty;

    public void RealizePen(XPen pen, PdfColorMode colorMode)
    {
      XColor color = pen.Color;
      color = ColorSpaceHelper.EnsureColorMode(colorMode, color);

      if (realizedLineWith != pen.width)
      {
        renderer.AppendFormat("{0:0.###} w\n", pen.width);
        realizedLineWith = pen.width;
      }

      if (realizedLineCap != (int)pen.lineCap)
      {
        renderer.AppendFormat("{0} J\n", (int)pen.lineCap);
        realizedLineCap = (int)pen.lineCap;
      }

      if (realizedLineJoin != (int)pen.lineJoin)
      {
        renderer.AppendFormat("{0} j\n", (int)pen.lineJoin);
        realizedLineJoin = (int)pen.lineJoin;
      }

      if (realizedLineCap == (int)XLineJoin.Miter)
      {
        if (realizedMiterLimit != (int)pen.miterLimit && (int)pen.miterLimit != 0)
        {
          renderer.AppendFormat("{0} M\n", (int)pen.miterLimit);
          realizedMiterLimit = (int)pen.miterLimit;
        }
      }

      if (realizedDashStyle != pen.dashStyle || pen.dashStyle == XDashStyle.Custom)
      {
        double dot = pen.Width;
        double dash = 3 * dot;

        // Line width 0 is not recommended but valid
        XDashStyle dashStyle = pen.DashStyle;
        if (dot == 0)
          dashStyle = XDashStyle.Solid;

        switch (dashStyle)
        {
          case XDashStyle.Solid:
            renderer.Append("[]0 d\n");
            break;

          case XDashStyle.Dash:
            renderer.AppendFormat("[{0:0.##} {1:0.##}]0 d\n", dash, dot);
            break;

          case XDashStyle.Dot:
            renderer.AppendFormat("[{0:0.##}]0 d\n", dot);
            break;

          case XDashStyle.DashDot:
            renderer.AppendFormat("[{0:0.##} {1:0.##} {1:0.##} {1:0.##}]0 d\n", dash, dot);
            break;

          case XDashStyle.DashDotDot:
            renderer.AppendFormat("[{0:0.##} {1:0.##} {1:0.##} {1:0.##} {1:0.##} {1:0.##}]0 d\n", dash, dot);
            break;

          case XDashStyle.Custom:
            {
              StringBuilder pdf = new StringBuilder("[", 256);
              int len = pen.dashPattern == null ? 0 : pen.dashPattern.Length;
              for (int idx = 0; idx < len; idx++)
              {
                if (idx > 0)
                  pdf.Append(' ');
                pdf.Append(PdfEncoders.ToString(pen.dashPattern[idx] * pen.width));
              }
              // Make an even number of values look like in GDI+
              if (len > 0 && len % 2 == 1)
              {
                pdf.Append(' ');
                pdf.Append(PdfEncoders.ToString(0.2 * pen.width));
              }
              pdf.AppendFormat(CultureInfo.InvariantCulture, "]{0:0.###} d\n", pen.dashOffset * pen.width);
              string pattern = pdf.ToString();

              // BUG: drice2@ageone.de reported a realizing problem
              // HACK: I romove the if clause
              //if (this.realizedDashPattern != pattern)
              {
                realizedDashPattern = pattern;
                renderer.Append(pattern);
              }
            }
            break;
        }
        realizedDashStyle = dashStyle;
      }

      if (colorMode != PdfColorMode.Cmyk)
      {
        if (realizedStrokeColor.Rgb != color.Rgb)
        {
          renderer.Append(PdfEncoders.ToString(color, PdfColorMode.Rgb));
          renderer.Append(" RG\n");
        }
      }
      else
      {
        if (!ColorSpaceHelper.IsEqualCmyk(realizedStrokeColor, color))
        {
          renderer.Append(PdfEncoders.ToString(color, PdfColorMode.Cmyk));
          renderer.Append(" K\n");
        }
      }

      if (renderer.Owner.Version >= 14 && realizedStrokeColor.A != color.A)
      {
        PdfExtGState extGState = renderer.Owner.ExtGStateTable.GetExtGStateStroke(color.A);
        string gs = renderer.Resources.AddExtGState(extGState);
        renderer.AppendFormat("{0} gs\n", gs);

        // Must create transparany group
        if (renderer.page != null && color.A < 1)
          renderer.page.transparencyUsed = true;
      }
      realizedStrokeColor = color;
    }

    #endregion

    #region Fill

    XColor realizedFillColor = XColor.Empty;

    public void RealizeBrush(XBrush brush, PdfColorMode colorMode)
    {
      if (brush is XSolidBrush)
      {
        XColor color = ((XSolidBrush)brush).Color;
        color = ColorSpaceHelper.EnsureColorMode(colorMode, color);

        if (colorMode != PdfColorMode.Cmyk)
        {
          if (realizedFillColor.Rgb != color.Rgb)
          {
            renderer.Append(PdfEncoders.ToString(color, PdfColorMode.Rgb));
            renderer.Append(" rg\n");
          }
        }
        else
        {
          if (!ColorSpaceHelper.IsEqualCmyk(realizedFillColor, color))
          {
            renderer.Append(PdfEncoders.ToString(color, PdfColorMode.Cmyk));
            renderer.Append(" k\n");
          }
        }

        if (renderer.Owner.Version >= 14 && realizedFillColor.A != color.A)
        {
          PdfExtGState extGState = renderer.Owner.ExtGStateTable.GetExtGStateNonStroke(color.A);
          string gs = renderer.Resources.AddExtGState(extGState);
          renderer.AppendFormat("{0} gs\n", gs);

          // Must create transparany group
          if (renderer.page != null && color.A < 1)
            renderer.page.transparencyUsed = true;
        }
        realizedFillColor = color;
      }
      else if (brush is XLinearGradientBrush)
      {
        XMatrix matrix = renderer.defaultViewMatrix;
        matrix.Prepend(Transform);
        PdfShadingPattern pattern = new PdfShadingPattern(renderer.Owner);
        pattern.SetupFromBrush((XLinearGradientBrush)brush, matrix);
        string name = renderer.Resources.AddPattern(pattern);
        renderer.AppendFormat("/Pattern cs\n", name);
        renderer.AppendFormat("{0} scn\n", name);

        // Invalidate fill color
        realizedFillColor = XColor.Empty;
      }
    }
    #endregion

    #region Text

    internal PdfFont realizedFont;
    string realizedFontName = String.Empty;
    double realizedFontSize;

    public void RealizeFont(XFont font, XBrush brush, int renderMode)
    {
      // So far rendering mode 0 only
      RealizeBrush(brush, renderer.colorMode); // this.renderer.page.document.Options.ColorMode);

      realizedFont = null;
      string fontName = renderer.GetFontName(font, out realizedFont);
      if (fontName != realizedFontName || realizedFontSize != font.Size)
      {
        if (renderer.Gfx.PageDirection == XPageDirection.Downwards)
          renderer.AppendFormat("{0} {1:0.###} Tf\n", fontName, -font.Size);
        else
          renderer.AppendFormat("{0} {1:0.###} Tf\n", fontName, font.Size);

        realizedFontName = fontName;
        realizedFontSize = font.Size;
      }
    }

    public XPoint realizedTextPosition;

    #endregion

    #region Transformation

    /// <summary>
    /// The realized current transformation matrix.
    /// </summary>
    private XMatrix realizedCtm;

    /// <summary>
    /// The unrealized current transformation matrix.
    /// </summary>
    XMatrix unrealizedCtm;

    /// <summary>
    /// A flag indicating whether the CTM must be realized.
    /// </summary>
    public bool MustRealizeCtm;

    public XMatrix Transform
    {
      get
      {
        if (MustRealizeCtm)
        {
          XMatrix matrix = realizedCtm;
          matrix.Prepend(unrealizedCtm);
          return matrix;
        }
        return realizedCtm;
      }
      set
      {
        XMatrix matrix = realizedCtm;
        matrix.Invert();
        matrix.Prepend(value);
        unrealizedCtm = matrix;
        MustRealizeCtm = !unrealizedCtm.IsIdentity;
      }
    }

    /// <summary>
    /// Modifies the current transformation matrix.
    /// </summary>
    public void MultiplyTransform(XMatrix matrix, XMatrixOrder order)
    {
      if (!matrix.IsIdentity)
      {
        MustRealizeCtm = true;
        unrealizedCtm.Multiply(matrix, order);
      }
    }

    /// <summary>
    /// Realizes the CTM.
    /// </summary>
    public void RealizeCtm()
    {
      if (MustRealizeCtm)
      {
        Debug.Assert(!unrealizedCtm.IsIdentity, "mrCtm is unnecessarily set.");

        double[] matrix = unrealizedCtm.GetElements();
        // Up to six decimal digits to prevent round up problems
        renderer.AppendFormat("{0:0.######} {1:0.######} {2:0.######} {3:0.######} {4:0.######} {5:0.######} cm\n",
          matrix[0], matrix[1], matrix[2], matrix[3], matrix[4], matrix[5]);

        realizedCtm.Prepend(unrealizedCtm);
        unrealizedCtm = new XMatrix();  //XMatrix.Identity;
        MustRealizeCtm = false;
      }
    }

    #endregion

    #region Clip Path

    public void SetAndRealizeClipRect(XRect clipRect)
    {
      XGraphicsPath clipPath = new XGraphicsPath();
      clipPath.AddRectangle(clipRect);
      RealizeClipPath(clipPath);
    }

    public void SetAndRealizeClipPath(XGraphicsPath clipPath)
    {
      RealizeClipPath(clipPath);
    }

    void RealizeClipPath(XGraphicsPath clipPath)
    {
      renderer.BeginGraphic();
      RealizeCtm();
#if GDI && !WPF
      this.renderer.AppendPath(clipPath.gdipPath);
#endif
#if WPF &&!GDI
      renderer.AppendPath(clipPath.pathGeometry);
#endif
#if WPF && GDI
      if (this.renderer.Gfx.targetContext == XGraphicTargetContext.GDI)
      {
        this.renderer.AppendPath(clipPath.gdipPath);
      }
      else
      {
        this.renderer.AppendPath(clipPath.pathGeometry);
      }
#endif
      if (clipPath.FillMode == XFillMode.Winding)
        renderer.Append("W n\n");
      else
        renderer.Append("W* n\n");
    }

    #endregion
  }
}
