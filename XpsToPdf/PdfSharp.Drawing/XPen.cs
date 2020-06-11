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
#if GDI
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
#if WPF
using System.Windows.Media;
#endif

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Defines an object used to draw lines and curves.
  /// </summary>
  public sealed class XPen
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="XPen"/> class.
    /// </summary>
    public XPen(XColor color)
      : this(color, 1, false)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="XPen"/> class.
    /// </summary>
    public XPen(XColor color, double width)
      : this(color, width, false)
    { }

    internal XPen(XColor color, double width, bool immutable)
    {
      this.color = color;
      this.width = width;
      lineJoin = XLineJoin.Miter;
      lineCap = XLineCap.Flat;
      dashStyle = XDashStyle.Solid;
      dashOffset = 0f;
      this.immutable = immutable;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XPen"/> class.
    /// </summary>
    public XPen(XPen pen)
    {
      color = pen.color;
      width = pen.width;
      lineJoin = pen.lineJoin;
      lineCap = pen.lineCap;
      dashStyle = pen.dashStyle;
      dashOffset = pen.dashOffset;
      dashPattern = pen.dashPattern;
      if (dashPattern != null)
        dashPattern = (double[])dashPattern.Clone();
    }

    /// <summary>
    /// Clones this instance.
    /// </summary>
    public XPen Clone()
    {
      return new XPen(this);
    }

    /// <summary>
    /// Gets or sets the color.
    /// </summary>
    public XColor Color
    {
      get => color;
      set
      {
        if (immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        dirty = dirty || color != value;
        color = value;
      }
    }
    internal XColor color;

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    public double Width
    {
      get => width;
      set
      {
        if (immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        dirty = dirty || width != value;
        width = value;
      }
    }
    internal double width;

    /// <summary>
    /// Gets or sets the line join.
    /// </summary>
    public XLineJoin LineJoin
    {
      get => lineJoin;
      set
      {
        if (immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        dirty = dirty || lineJoin != value;
        lineJoin = value;
      }
    }
    internal XLineJoin lineJoin;

    /// <summary>
    /// Gets or sets the line cap.
    /// </summary>
    public XLineCap LineCap
    {
      get => lineCap;
      set
      {
        if (immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        dirty = dirty || lineCap != value;
        lineCap = value;
      }
    }
    internal XLineCap lineCap;

    /// <summary>
    /// Gets or sets the miter limit.
    /// </summary>
    public double MiterLimit
    {
      get => miterLimit;
      set
      {
        if (immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        dirty = dirty || miterLimit != value;
        miterLimit = value;
      }
    }
    internal double miterLimit;

    /// <summary>
    /// Gets or sets the dash style.
    /// </summary>
    public XDashStyle DashStyle
    {
      get => dashStyle;
      set
      {
        if (immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        dirty = dirty || dashStyle != value;
        dashStyle = value;
      }
    }
    internal XDashStyle dashStyle;

    /// <summary>
    /// Gets or sets the dash offset.
    /// </summary>
    public double DashOffset
    {
      get => dashOffset;
      set
      {
        if (immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        dirty = dirty || dashOffset != value;
        dashOffset = value;
      }
    }
    internal double dashOffset;

    /// <summary>
    /// Gets or sets the dash pattern.
    /// </summary>
    public double[] DashPattern
    {
      get
      {
        if (dashPattern == null)
          dashPattern = new double[0];
        return dashPattern;
      }
      set
      {
        if (immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));

        int length = value.Length;
        //if (length == 0)
        //  throw new ArgumentException("Dash pattern array must not be empty.");

        for (int idx = 0; idx < length; idx++)
        {
          if (value[idx] <= 0)
            throw new ArgumentException("Dash pattern value must greater than zero.");
        }

        dirty = true;
        dashStyle = XDashStyle.Custom;
        dashPattern = (double[])value.Clone();
      }
    }
    internal double[] dashPattern;

#if GDI
#if UseGdiObjects
    /// <summary>
    /// Implicit conversion from Pen to XPen
    /// </summary>
    public static implicit operator XPen(Pen pen)
    {
      XPen xpen;
      switch (pen.PenType)
      {
        case PenType.SolidColor:
          xpen = new XPen(pen.Color, pen.Width);
          xpen.LineJoin = (XLineJoin)pen.LineJoin;
          xpen.DashStyle = (XDashStyle)pen.DashStyle;
          xpen.miterLimit = pen.MiterLimit;
          break;

        default:
          throw new NotImplementedException("Pen type not supported by PDFsharp.");
      }
      // Bug fixed by drice2@ageone.de
      if (pen.DashStyle == System.Drawing.Drawing2D.DashStyle.Custom)
      {
        int length = pen.DashPattern.Length;
        double[] pattern = new double[length];
        for (int idx = 0; idx < length; idx++)
          pattern[idx] = pen.DashPattern[idx];
        xpen.DashPattern = pattern;
        xpen.dashOffset = pen.DashOffset;
      }
      return xpen;
    }
#endif

    internal System.Drawing.Pen RealizeGdiPen()
    {
      if (this.dirty)
      {
        if (this.gdiPen == null)
          this.gdiPen = new System.Drawing.Pen(this.color.ToGdiColor(), (float)this.width);
        else
        {
          this.gdiPen.Color = this.color.ToGdiColor();
          this.gdiPen.Width = (float)this.width;
        }
        LineCap lineCap = XConvert.ToLineCap(this.lineCap);
        this.gdiPen.StartCap = lineCap;
        this.gdiPen.EndCap = lineCap;
        this.gdiPen.LineJoin = XConvert.ToLineJoin(this.lineJoin);
        this.gdiPen.DashOffset = (float)this.dashOffset;
        if (this.dashStyle == XDashStyle.Custom)
        {
          int len = this.dashPattern == null ? 0 : this.dashPattern.Length;
          float[] pattern = new float[len];
          for (int idx = 0; idx < len; idx++)
            pattern[idx] = (float)this.dashPattern[idx];
          this.gdiPen.DashPattern = pattern;
        }
        else
          this.gdiPen.DashStyle = (System.Drawing.Drawing2D.DashStyle)this.dashStyle;
      }
      return this.gdiPen;
    }
#endif

#if WPF
    internal Pen RealizeWpfPen()
    {
#if !SILVERLIGHT
      if (dirty || !dirty) // TODOWPF: XPen is frozen by design, WPF Pen can change
      {
        //if (this.wpfPen == null)
        wpfPen = new Pen(new SolidColorBrush(color.ToWpfColor()), width);
        //else
        //{
        //  this.wpfPen.Brush = new SolidColorBrush(this.color.ToWpfColor());
        //  this.wpfPen.Thickness = this.width;
        //}
        PenLineCap lineCap = XConvert.ToPenLineCap(this.lineCap);
        wpfPen.StartLineCap = lineCap;
        wpfPen.EndLineCap = lineCap;
        wpfPen.LineJoin = XConvert.ToPenLineJoin(lineJoin);
        if (dashStyle == XDashStyle.Custom)
        {
          // TODOWPF: does not work in all cases
          wpfPen.DashStyle = new DashStyle(dashPattern, dashOffset);
        }
        else
        {
          switch (dashStyle)
          {
            case XDashStyle.Solid:
              wpfPen.DashStyle = DashStyles.Solid;
              break;

            case XDashStyle.Dash:
              //this.wpfPen.DashStyle = DashStyles.Dash;
              wpfPen.DashStyle = new DashStyle(new double[] { 2, 2 }, 0);
              break;

            case XDashStyle.Dot:
              //this.wpfPen.DashStyle = DashStyles.Dot;
              wpfPen.DashStyle = new DashStyle(new double[] { 0, 2 }, 1.5);
              break;

            case XDashStyle.DashDot:
              //this.wpfPen.DashStyle = DashStyles.DashDot;
              wpfPen.DashStyle = new DashStyle(new double[] { 2, 2, 0, 2 }, 0);
              break;

            case XDashStyle.DashDotDot:
              //this.wpfPen.DashStyle = DashStyles.DashDotDot;
              wpfPen.DashStyle = new DashStyle(new double[] { 2, 2, 0, 2, 0, 2 }, 0);
              break;
          }
        }
      }
#else
      this.wpfPen = new System.Windows.Media.Pen(new SolidColorBrush(this.color.ToWpfColor()), this.width);
#endif
      return wpfPen;
    }
#endif

    bool dirty = true;
    bool immutable;
#if GDI
    System.Drawing.Pen gdiPen;
#endif
#if WPF //&& !SILVERLIGHT
    Pen wpfPen;
#endif
  }
}