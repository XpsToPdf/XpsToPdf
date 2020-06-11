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
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
#if GDI
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
#if WPF
using System.Windows.Media;
#endif
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;

// WPFHACK
#pragma warning disable 162

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Defines an object used to draw text.
  /// </summary>
  [DebuggerDisplay("'{Name}', {Size}")]
  public class XFont
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="XFont"/> class.
    /// </summary>
    /// <param name="familyName">Name of the font family.</param>
    /// <param name="emSize">The em size.</param>
    public XFont(string familyName, double emSize)
    {
      this.familyName = familyName;
      this.emSize = emSize;
      style = XFontStyle.Regular;
      pdfOptions = new XPdfFontOptions();
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XFont"/> class.
    /// </summary>
    /// <param name="familyName">Name of the font family.</param>
    /// <param name="emSize">The em size.</param>
    /// <param name="style">The font style.</param>
    public XFont(string familyName, double emSize, XFontStyle style)
    {
      this.familyName = familyName;
      this.emSize = emSize;
      this.style = style;
      pdfOptions = new XPdfFontOptions();
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XFont"/> class.
    /// </summary>
    /// <param name="familyName">Name of the font family.</param>
    /// <param name="emSize">The em size.</param>
    /// <param name="style">The font style.</param>
    /// <param name="pdfOptions">Additional PDF options.</param>
    public XFont(string familyName, double emSize, XFontStyle style, XPdfFontOptions pdfOptions)
    {
      this.familyName = familyName;
      this.emSize = emSize;
      this.style = style;
      this.pdfOptions = pdfOptions;
      Initialize();
    }

#if GDI // #PFC
    /// <summary>
    /// Initializes a new instance of the <see cref="XFont"/> class.
    /// </summary>
    /// <param name="family">The font family.</param>
    /// <param name="emSize">The em size.</param>
    /// <param name="style">The font style.</param>
    /// <param name="pdfOptions">Additional PDF options.</param>
    public XFont(System.Drawing.FontFamily family, double emSize, XFontStyle style, XPdfFontOptions pdfOptions /*,
      XPrivateFontCollection privateFontCollection*/
                                                    )
    {
      this.familyName = null;
      this.gdifamily = family;
      this.emSize = emSize;
      this.style = style;
      this.pdfOptions = pdfOptions;
      Initialize();
    }
#endif

#if GDI
#if UseGdiObjects
    /// <summary>
    /// Initializes a new instance of the <see cref="XFont"/> class from a System.Drawing.Font.
    /// </summary>
    /// <param name="font">A System.Drawing.Font.</param>
    /// <param name="pdfOptions">Additional PDF options.</param>
    public XFont(Font font, XPdfFontOptions pdfOptions)
    {
      if (font.Unit != GraphicsUnit.World)
        throw new ArgumentException("Font must use GraphicsUnit.World.");
      this.font = font;
      this.familyName = font.Name;
      this.emSize = font.Size;
      this.style = FontStyleFrom(font);
      this.pdfOptions = pdfOptions;
      Initialize();
    }
#endif
#endif

    /// <summary>
    /// Connects the specifications of a font from XFont to a real glyph type face.
    /// </summary>
    void Initialize()
    {
      XFontMetrics fm = null;

#if DEBUG___
      FontData[] fontDataArray = FontDataStock.Global.GetFontDataList();
      if (fontDataArray.Length > 0)
      {
        ////        GetType();
        ////#if GDI
        ////        var x = XPrivateFontCollection.global.GlobalPrivateFontCollection;
        ////        families = x.Families;

        ////        bool fff = families[0].IsStyleAvailable(System.Drawing.FontStyle.Regular);
        ////        fff.GetType();
        ////        this.font = new Font(families[0].Name, 12, System.Drawing.FontStyle.Regular, GraphicsUnit.Pixel);

        ////        this.font = new Font("Oblivious", 12, System.Drawing.FontStyle.Regular, GraphicsUnit.Pixel);

        ////        this.font = new Font(families[0], 12, System.Drawing.FontStyle.Regular, GraphicsUnit.Pixel);

        ////        System.Drawing.FontFamily f = new System.Drawing.FontFamily(families[0].Name);
        ////        f.GetType();
        ////#endif
      }
#endif
#if GDI
      if (this.font == null)
      {
        if (this.gdifamily != null)
        {
          this.font = new Font(this.gdifamily, (float)this.emSize, (System.Drawing.FontStyle)this.style, GraphicsUnit.World);
          this.familyName = this.gdifamily.Name; // Do we need this???
        }
        else
        {
          // First check private fonts
          this.font = XPrivateFontCollection.TryFindPrivateFont(this.familyName, this.emSize, (System.Drawing.FontStyle)this.style) ??
            new Font(this.familyName, (float)this.emSize, (System.Drawing.FontStyle)this.style, GraphicsUnit.World);
        }
#if DEBUG
        // new Font returns MSSansSerif if the requested font was not found ...
        //Debug.Assert(this.familyName == this.font.FontFamily.Name);
#endif
      }

      fm = Metrics;
      System.Drawing.FontFamily fontFamily = this.font.FontFamily;
      this.unitsPerEm = fm.UnitsPerEm;

      System.Drawing.FontFamily fontFamily2 = this.font.FontFamily;
      this.cellSpace = fontFamily2.GetLineSpacing(font.Style);
      //Debug.Assert(this.cellSpace == fm.Ascent + Math.Abs(fm.Descent) + fm.Leading, "Value differs from information retrieved from font image.");

      this.cellAscent = fontFamily.GetCellAscent(font.Style);
#pragma warning disable 1030
#warning delTHHO
      //!!!delTHHO 14.08.2008 Debug.Assert(this.cellAscent == fm.Ascent, "Value differs from information retrieved from font image.");
      //Debug.Assert(this.cellAscent == fm.Ascent, "Value differs from information retrieved from font image.");

      this.cellDescent = fontFamily.GetCellDescent(font.Style);
#if DEBUG
      int desc = Math.Abs(fm.Descent);
      if (this.cellDescent != desc)
        Debug.Assert(false, "Value differs from information retrieved from font image.");
#endif
#endif
#if WPF
#if !SILVERLIGHT
      if (family == null)
      {
        Debug.Assert(typeface == null);
        typeface = XPrivateFontCollection.TryFindTypeface(Name, style, out family);
#if true
        if (typeface != null)
        {
          GlyphTypeface glyphTypeface;

          ICollection<Typeface> list = family.GetTypefaces();
          foreach (Typeface tf in list)
          {
            if (!tf.TryGetGlyphTypeface(out glyphTypeface))
              Debugger.Break();
          }

          if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
            throw new InvalidOperationException(PSSR.CannotGetGlyphTypeface(Name));
        }
#endif
      }

      if (family == null)
        family = new FontFamily(Name);

      if (typeface == null)
        typeface = FontHelper.CreateTypeface(family, style);

      fm = Metrics;
      Debug.Assert(unitsPerEm == 0 || unitsPerEm == fm.UnitsPerEm);
      unitsPerEm = fm.UnitsPerEm;

      //Debug.Assert(this.cellSpace == 0 || this.cellSpace == fm.Ascent + Math.Abs(fm.Descent) + fm.Leading);
      cellSpace = fm.Ascent + Math.Abs(fm.Descent) + fm.Leading;

      Debug.Assert(cellAscent == 0 || cellAscent == fm.Ascent);
      cellAscent = fm.Ascent;

      Debug.Assert(cellDescent == 0 || cellDescent == Math.Abs(fm.Descent));
      cellDescent = Math.Abs(fm.Descent);
#else
      if (fm != null)
        fm.GetType();
#endif
#endif
    }

#if GDI
    // Fonts can be created from familyName or from family!
    //string familyName;
    /// <summary>
    /// Gets the GDI family.
    /// </summary>
    /// <value>The GDI family.</value>
    public System.Drawing.FontFamily GdiFamily
    {
      get { return this.gdifamily; }
      //set { this.gdifamily = value; }
    }
    System.Drawing.FontFamily gdifamily;
#endif

#if GDI
    internal static XFontStyle FontStyleFrom(Font font)
    {
      return
        (font.Bold ? XFontStyle.Bold : 0) |
        (font.Italic ? XFontStyle.Italic : 0) |
        (font.Strikeout ? XFontStyle.Strikeout : 0) |
        (font.Underline ? XFontStyle.Underline : 0);
    }
#endif

    //// Methods
    //public Font(Font prototype, FontStyle newStyle);
    //public Font(FontFamily family, float emSize);
    //public Font(string familyName, float emSize);
    //public Font(FontFamily family, float emSize, FontStyle style);
    //public Font(FontFamily family, float emSize, GraphicsUnit unit);
    //public Font(string familyName, float emSize, FontStyle style);
    //public Font(string familyName, float emSize, GraphicsUnit unit);
    //public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit);
    //public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit);
    ////public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet);
    ////public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet);
    ////public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont);
    ////public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont);


    //public object Clone();
    //private static FontFamily CreateFontFamilyWithFallback(string familyName);

    //private void Dispose(bool disposing);
    //public override bool Equals(object obj);
    //protected override void Finalize();
    //public static Font FromHdc(IntPtr hdc);
    //public static Font FromHfont(IntPtr hfont);
    //public static Font FromLogFont(object lf);
    //public static Font FromLogFont(object lf, IntPtr hdc);
    //public override int GetHashCode();

    /// <summary>
    /// Returns the line spacing, in pixels, of this font. The line spacing is the vertical distance
    /// between the base lines of two consecutive lines of text. Thus, the line spacing includes the
    /// blank space between lines along with the height of the character itself.
    /// </summary>
    public double GetHeight()
    {
#if GDI
      RealizeGdiFont();
      double gdiValue = this.font.GetHeight();
#if DEBUG
      float myValue = (float)(this.cellSpace * this.emSize / this.unitsPerEm);
      //Debug.Assert(DoubleUtil.AreClose((float)value, myValue), "Check formula.");
      Debug.Assert(DoubleUtil.AreRoughlyEqual(gdiValue, myValue, 5), "Check formula.");

      //// 2355*(0.3/2048)*96 = 33.11719 
      //double myValue = this.cellSpace * (this.size / 72 / this.unitsPerEm) * 96;
      //double myValue2 = (float)(this.cellSpace * (this.size / /*72 /*/ this.unitsPerEm) /* 72*/);
      //Int64 i1 = (Int64)(value * 1000 + .5);
      //Int64 i2 = (Int64)(myValue2 * 1000 + .5);
      //Debug.Assert(i1 == i2, "??");
#endif
      return gdiValue;
#endif
#if WPF
      double value = cellSpace * emSize / unitsPerEm;
      return value;
#endif
    }

    /// <summary>
    /// Returns the line spacing, in the current unit of a specified Graphics object, of this font.
    /// The line spacing is the vertical distance between the base lines of two consecutive lines of
    /// text. Thus, the line spacing includes the blank space between lines along with the height of
    /// </summary>
    public double GetHeight(XGraphics graphics)
    {
#if GDI && !WPF
      RealizeGdiFont();
      double value = this.font.GetHeight(graphics.gfx);
      Debug.Assert(value == this.font.GetHeight(graphics.gfx.DpiY));
      double value2 = this.cellSpace * this.emSize / this.unitsPerEm;
      Debug.Assert(value - value2 < 1e-3, "??");
      return this.font.GetHeight(graphics.gfx);
#endif
#if WPF && !GDI
      double value = cellSpace * emSize / unitsPerEm;
      return value;
#endif
#if GDI && WPF
      if (graphics.targetContext == XGraphicTargetContext.GDI)
      {
        RealizeGdiFont();
#if DEBUG
        double value = this.font.GetHeight(graphics.gfx);

        // 2355*(0.3/2048)*96 = 33.11719 
        double myValue = this.cellSpace * (this.emSize / (96 * this.unitsPerEm)) * 96;
        myValue = this.cellSpace * this.emSize / this.unitsPerEm;
        //Debug.Assert(value == myValue, "??");
        //Debug.Assert(value - myValue < 1e-3, "??");
#endif
        return this.font.GetHeight(graphics.gfx);
      }
      else if (graphics.targetContext == XGraphicTargetContext.WPF)
      {
        double value = this.cellSpace * this.emSize / this.unitsPerEm;
        return value;
      }
      Debug.Assert(false);
      return 0;
#endif
    }

    //public float GetHeight(float dpi);
    //public IntPtr ToHfont();
    //public void ToLogFont(object logFont);
    //public void ToLogFont(object logFont, Graphics graphics);
    //public override string ToString();

    // Properties

    /// <summary>
    /// Gets the XFontFamily object associated with this XFont object.
    /// </summary>
    [Browsable(false)]
    public XFontFamily FontFamily
    {
      get
      {
        if (fontFamily == null)
        {
#if GDI
          RealizeGdiFont();
          this.fontFamily = new XFontFamily(this.font.FontFamily);
#endif
#if WPF
#if !SILVERLIGHT
          Debug.Assert(family != null);
          fontFamily = new XFontFamily(family);
#else
          // AGHACK
#endif
#endif
        }
        return fontFamily;
      }
    }
    XFontFamily fontFamily;

    /// <summary>
    /// Gets the face name of this Font object.
    /// </summary>
    public string Name
    {
      get
      {
#if GDI
        RealizeGdiFont();
        return this.font.Name;
#endif
#if WPF || SILVERLIGHT
        //RealizeGdiFont();
        return familyName;
#endif
      }
    }

    /// <summary>
    /// Gets the em-size of this Font object measured in the unit of this Font object.
    /// </summary>
    public double Size => emSize;

    double emSize;


    /// <summary>
    /// Gets the line spacing of this font.
    /// </summary>
    [Browsable(false)]
    public int Height =>
        // Implementation from System.Drawing.Font.cs
        (int)Math.Ceiling(GetHeight());

    // DELETE
    //      {
    //#if GDI && !WPF
    //        RealizeGdiFont();
    //        return this.font.Height;
    //#endif
    //#if WPF && !GDI
    //        return (int)this.size;
    //#endif
    //#if GDI && WPF
    //        // netmassdownloader -d "C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5" -output G:\dotnet-massdownload\SourceCode -v
    //        RealizeGdiFont();
    //        int gdiHeight = this.font.Height;
    //        double wpfHeight1 = this.cellSpace * this.size / this.unitsPerEm;
    //        //int wpfHeight = (int)wpfHeight1+Math.Round(wpfHeight1, 
    //        int wpfHeight = Convert.ToInt32(wpfHeight1 + 0.5);
    //        Debug.Assert(gdiHeight == wpfHeight);
    //        return gdiHeight;
    //#endif
    //      }
    /// <summary>
    /// Gets style information for this Font object.
    /// </summary>
    [Browsable(false)]
    public XFontStyle Style => style;

    XFontStyle style;

    /// <summary>
    /// Indicates whether this XFont object is bold.
    /// </summary>
    public bool Bold => (style & XFontStyle.Bold) == XFontStyle.Bold;

    /// <summary>
    /// Indicates whether this XFont object is italic.
    /// </summary>
    public bool Italic => (style & XFontStyle.Italic) == XFontStyle.Italic;

    /// <summary>
    /// Indicates whether this XFont object is stroke out.
    /// </summary>
    public bool Strikeout => (style & XFontStyle.Strikeout) == XFontStyle.Strikeout;

    /// <summary>
    /// Indicates whether this XFont object is underlined.
    /// </summary>
    public bool Underline => (style & XFontStyle.Underline) == XFontStyle.Underline;

    /// <summary>
    /// Temporary HACK for XPS to PDF converter.
    /// </summary>
    internal bool IsVertical
    {
      get => isVertical;
      set => isVertical = value;
    }
    bool isVertical;


    /// <summary>
    /// Gets the PDF options of the font.
    /// </summary>
    public XPdfFontOptions PdfOptions
    {
      get
      {
        if (pdfOptions == null)
          pdfOptions = new XPdfFontOptions();
        return pdfOptions;
      }
    }
    XPdfFontOptions pdfOptions;

    /// <summary>
    /// Indicates whether this XFont is encoded as Unicode.
    /// </summary>
    internal bool Unicode => pdfOptions != null ? pdfOptions.FontEncoding == PdfFontEncoding.Unicode : false;

    /// <summary>
    /// Gets the metrics.
    /// </summary>
    /// <value>The metrics.</value>
    public XFontMetrics Metrics
    {
      get
      {
        if (fontMetrics == null)
        {
          FontDescriptor descriptor = FontDescriptorStock.Global.CreateDescriptor(this);
          fontMetrics = descriptor.FontMetrics;
        }
        return fontMetrics;
      }
    }
    XFontMetrics fontMetrics;

#if GDI
#if UseGdiObjects
    /// <summary>
    /// Implicit conversion form Font to XFont
    /// </summary>
    public static implicit operator XFont(Font font)
    {
      //XFont xfont = new XFont(font.Name, font.Size, FontStyleFrom(font));
      XFont xfont = new XFont(font, null);
      return xfont;
    }
#endif

    internal Font RealizeGdiFont()
    {
      //if (this.font == null)
      //  this.font = new Font(this.familyName, this.size, (FontStyle)this.style);
      return this.font;
    }
    internal Font font;
#endif

#if WPF && !SILVERLIGHT
    internal Typeface RealizeWpfTypeface()
    {
      return typeface;
    }
    internal FontFamily family;
    internal Typeface typeface;
#endif

    internal string familyName;
    internal int unitsPerEm;
    internal int cellSpace;
    internal int cellAscent;
    internal int cellDescent;

    /// <summary>
    /// Cache PdfFontTable.FontSelector to speed up finding the right PdfFont
    /// if this font is used more than once.
    /// </summary>
    internal PdfFontTable.FontSelector selector;
  }
}
