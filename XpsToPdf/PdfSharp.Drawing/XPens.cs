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

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Pens for all the pre-defined colors.
  /// </summary>
  public static class XPens
  {
    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen AliceBlue
    {
      get
      {
        if (aliceBlue == null)
          aliceBlue = new XPen(XColors.AliceBlue, 1, true);
        return aliceBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen AntiqueWhite
    {
      get
      {
        if (antiqueWhite == null)
          antiqueWhite = new XPen(XColors.AntiqueWhite, 1, true);
        return antiqueWhite;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Aqua
    {
      get
      {
        if (aqua == null)
          aqua = new XPen(XColors.Aqua, 1, true);
        return aqua;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Aquamarine
    {
      get
      {
        if (aquamarine == null)
          aquamarine = new XPen(XColors.Aquamarine, 1, true);
        return aquamarine;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Azure
    {
      get
      {
        if (azure == null)
          azure = new XPen(XColors.Azure, 1, true);
        return azure;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Beige
    {
      get
      {
        if (beige == null)
          beige = new XPen(XColors.Beige, 1, true);
        return beige;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Bisque
    {
      get
      {
        if (bisque == null)
          bisque = new XPen(XColors.Bisque, 1, true);
        return bisque;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Black
    {
      get
      {
        if (black == null)
          black = new XPen(XColors.Black, 1, true);
        return black;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen BlanchedAlmond
    {
      get
      {
        if (blanchedAlmond == null)
          blanchedAlmond = new XPen(XColors.BlanchedAlmond, 1, true);
        return blanchedAlmond;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Blue
    {
      get
      {
        if (blue == null)
          blue = new XPen(XColors.Blue, 1, true);
        return blue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen BlueViolet
    {
      get
      {
        if (blueViolet == null)
          blueViolet = new XPen(XColors.BlueViolet, 1, true);
        return blueViolet;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Brown
    {
      get
      {
        if (brown == null)
          brown = new XPen(XColors.Brown, 1, true);
        return brown;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen BurlyWood
    {
      get
      {
        if (burlyWood == null)
          burlyWood = new XPen(XColors.BurlyWood, 1, true);
        return burlyWood;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen CadetBlue
    {
      get
      {
        if (cadetBlue == null)
          cadetBlue = new XPen(XColors.CadetBlue, 1, true);
        return cadetBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Chartreuse
    {
      get
      {
        if (chartreuse == null)
          chartreuse = new XPen(XColors.Chartreuse, 1, true);
        return chartreuse;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Chocolate
    {
      get
      {
        if (chocolate == null)
          chocolate = new XPen(XColors.Chocolate, 1, true);
        return chocolate;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Coral
    {
      get
      {
        if (coral == null)
          coral = new XPen(XColors.Coral, 1, true);
        return coral;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen CornflowerBlue
    {
      get
      {
        if (cornflowerBlue == null)
          cornflowerBlue = new XPen(XColors.CornflowerBlue, 1, true);
        return cornflowerBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Cornsilk
    {
      get
      {
        if (cornsilk == null)
          cornsilk = new XPen(XColors.Cornsilk, 1, true);
        return cornsilk;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Crimson
    {
      get
      {
        if (crimson == null)
          crimson = new XPen(XColors.Crimson, 1, true);
        return crimson;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Cyan
    {
      get
      {
        if (cyan == null)
          cyan = new XPen(XColors.Cyan, 1, true);
        return cyan;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkBlue
    {
      get
      {
        if (darkBlue == null)
          darkBlue = new XPen(XColors.DarkBlue, 1, true);
        return darkBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkCyan
    {
      get
      {
        if (darkCyan == null)
          darkCyan = new XPen(XColors.DarkCyan, 1, true);
        return darkCyan;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkGoldenrod
    {
      get
      {
        if (darkGoldenrod == null)
          darkGoldenrod = new XPen(XColors.DarkGoldenrod, 1, true);
        return darkGoldenrod;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkGray
    {
      get
      {
        if (darkGray == null)
          darkGray = new XPen(XColors.DarkGray, 1, true);
        return darkGray;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkGreen
    {
      get
      {
        if (darkGreen == null)
          darkGreen = new XPen(XColors.DarkGreen, 1, true);
        return darkGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkKhaki
    {
      get
      {
        if (darkKhaki == null)
          darkKhaki = new XPen(XColors.DarkKhaki, 1, true);
        return darkKhaki;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkMagenta
    {
      get
      {
        if (darkMagenta == null)
          darkMagenta = new XPen(XColors.DarkMagenta, 1, true);
        return darkMagenta;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkOliveGreen
    {
      get
      {
        if (darkOliveGreen == null)
          darkOliveGreen = new XPen(XColors.DarkOliveGreen, 1, true);
        return darkOliveGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkOrange
    {
      get
      {
        if (darkOrange == null)
          darkOrange = new XPen(XColors.DarkOrange, 1, true);
        return darkOrange;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkOrchid
    {
      get
      {
        if (darkOrchid == null)
          darkOrchid = new XPen(XColors.DarkOrchid, 1, true);
        return darkOrchid;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkRed
    {
      get
      {
        if (darkRed == null)
          darkRed = new XPen(XColors.DarkRed, 1, true);
        return darkRed;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkSalmon
    {
      get
      {
        if (darkSalmon == null)
          darkSalmon = new XPen(XColors.DarkSalmon, 1, true);
        return darkSalmon;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkSeaGreen
    {
      get
      {
        if (darkSeaGreen == null)
          darkSeaGreen = new XPen(XColors.DarkSeaGreen, 1, true);
        return darkSeaGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkSlateBlue
    {
      get
      {
        if (darkSlateBlue == null)
          darkSlateBlue = new XPen(XColors.DarkSlateBlue, 1, true);
        return darkSlateBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkSlateGray
    {
      get
      {
        if (darkSlateGray == null)
          darkSlateGray = new XPen(XColors.DarkSlateGray, 1, true);
        return darkSlateGray;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkTurquoise
    {
      get
      {
        if (darkTurquoise == null)
          darkTurquoise = new XPen(XColors.DarkTurquoise, 1, true);
        return darkTurquoise;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DarkViolet
    {
      get
      {
        if (darkViolet == null)
          darkViolet = new XPen(XColors.DarkViolet, 1, true);
        return darkViolet;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DeepPink
    {
      get
      {
        if (deepPink == null)
          deepPink = new XPen(XColors.DeepPink, 1, true);
        return deepPink;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DeepSkyBlue
    {
      get
      {
        if (deepSkyBlue == null)
          deepSkyBlue = new XPen(XColors.DeepSkyBlue, 1, true);
        return deepSkyBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DimGray
    {
      get
      {
        if (dimGray == null)
          dimGray = new XPen(XColors.DimGray, 1, true);
        return dimGray;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen DodgerBlue
    {
      get
      {
        if (dodgerBlue == null)
          dodgerBlue = new XPen(XColors.DodgerBlue, 1, true);
        return dodgerBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Firebrick
    {
      get
      {
        if (firebrick == null)
          firebrick = new XPen(XColors.Firebrick, 1, true);
        return firebrick;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen FloralWhite
    {
      get
      {
        if (floralWhite == null)
          floralWhite = new XPen(XColors.FloralWhite, 1, true);
        return floralWhite;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen ForestGreen
    {
      get
      {
        if (forestGreen == null)
          forestGreen = new XPen(XColors.ForestGreen, 1, true);
        return forestGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Fuchsia
    {
      get
      {
        if (fuchsia == null)
          fuchsia = new XPen(XColors.Fuchsia, 1, true);
        return fuchsia;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Gainsboro
    {
      get
      {
        if (gainsboro == null)
          gainsboro = new XPen(XColors.Gainsboro, 1, true);
        return gainsboro;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen GhostWhite
    {
      get
      {
        if (ghostWhite == null)
          ghostWhite = new XPen(XColors.GhostWhite, 1, true);
        return ghostWhite;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Gold
    {
      get
      {
        if (gold == null)
          gold = new XPen(XColors.Gold, 1, true);
        return gold;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Goldenrod
    {
      get
      {
        if (goldenrod == null)
          goldenrod = new XPen(XColors.Goldenrod, 1, true);
        return goldenrod;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Gray
    {
      get
      {
        if (gray == null)
          gray = new XPen(XColors.Gray, 1, true);
        return gray;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Green
    {
      get
      {
        if (green == null)
          green = new XPen(XColors.Green, 1, true);
        return green;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen GreenYellow
    {
      get
      {
        if (greenYellow == null)
          greenYellow = new XPen(XColors.GreenYellow, 1, true);
        return greenYellow;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Honeydew
    {
      get
      {
        if (honeydew == null)
          honeydew = new XPen(XColors.Honeydew, 1, true);
        return honeydew;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen HotPink
    {
      get
      {
        if (hotPink == null)
          hotPink = new XPen(XColors.HotPink, 1, true);
        return hotPink;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen IndianRed
    {
      get
      {
        if (indianRed == null)
          indianRed = new XPen(XColors.IndianRed, 1, true);
        return indianRed;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Indigo
    {
      get
      {
        if (indigo == null)
          indigo = new XPen(XColors.Indigo, 1, true);
        return indigo;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Ivory
    {
      get
      {
        if (ivory == null)
          ivory = new XPen(XColors.Ivory, 1, true);
        return ivory;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Khaki
    {
      get
      {
        if (khaki == null)
          khaki = new XPen(XColors.Khaki, 1, true);
        return khaki;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Lavender
    {
      get
      {
        if (lavender == null)
          lavender = new XPen(XColors.Lavender, 1, true);
        return lavender;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LavenderBlush
    {
      get
      {
        if (lavenderBlush == null)
          lavenderBlush = new XPen(XColors.LavenderBlush, 1, true);
        return lavenderBlush;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LawnGreen
    {
      get
      {
        if (lawnGreen == null)
          lawnGreen = new XPen(XColors.LawnGreen, 1, true);
        return lawnGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LemonChiffon
    {
      get
      {
        if (lemonChiffon == null)
          lemonChiffon = new XPen(XColors.LemonChiffon, 1, true);
        return lemonChiffon;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightBlue
    {
      get
      {
        if (lightBlue == null)
          lightBlue = new XPen(XColors.LightBlue, 1, true);
        return lightBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightCoral
    {
      get
      {
        if (lightCoral == null)
          lightCoral = new XPen(XColors.LightCoral, 1, true);
        return lightCoral;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightCyan
    {
      get
      {
        if (lightCyan == null)
          lightCyan = new XPen(XColors.LightCyan, 1, true);
        return lightCyan;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightGoldenrodYellow
    {
      get
      {
        if (lightGoldenrodYellow == null)
          lightGoldenrodYellow = new XPen(XColors.LightGoldenrodYellow, 1, true);
        return lightGoldenrodYellow;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightGray
    {
      get
      {
        if (lightGray == null)
          lightGray = new XPen(XColors.LightGray, 1, true);
        return lightGray;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightGreen
    {
      get
      {
        if (lightGreen == null)
          lightGreen = new XPen(XColors.LightGreen, 1, true);
        return lightGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightPink
    {
      get
      {
        if (lightPink == null)
          lightPink = new XPen(XColors.LightPink, 1, true);
        return lightPink;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightSalmon
    {
      get
      {
        if (lightSalmon == null)
          lightSalmon = new XPen(XColors.LightSalmon, 1, true);
        return lightSalmon;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightSeaGreen
    {
      get
      {
        if (lightSeaGreen == null)
          lightSeaGreen = new XPen(XColors.LightSeaGreen, 1, true);
        return lightSeaGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightSkyBlue
    {
      get
      {
        if (lightSkyBlue == null)
          lightSkyBlue = new XPen(XColors.LightSkyBlue, 1, true);
        return lightSkyBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightSlateGray
    {
      get
      {
        if (lightSlateGray == null)
          lightSlateGray = new XPen(XColors.LightSlateGray, 1, true);
        return lightSlateGray;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightSteelBlue
    {
      get
      {
        if (lightSteelBlue == null)
          lightSteelBlue = new XPen(XColors.LightSteelBlue, 1, true);
        return lightSteelBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LightYellow
    {
      get
      {
        if (lightYellow == null)
          lightYellow = new XPen(XColors.LightYellow, 1, true);
        return lightYellow;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Lime
    {
      get
      {
        if (lime == null)
          lime = new XPen(XColors.Lime, 1, true);
        return lime;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen LimeGreen
    {
      get
      {
        if (limeGreen == null)
          limeGreen = new XPen(XColors.LimeGreen, 1, true);
        return limeGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Linen
    {
      get
      {
        if (linen == null)
          linen = new XPen(XColors.Linen, 1, true);
        return linen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Magenta
    {
      get
      {
        if (magenta == null)
          magenta = new XPen(XColors.Magenta, 1, true);
        return magenta;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Maroon
    {
      get
      {
        if (maroon == null)
          maroon = new XPen(XColors.Maroon, 1, true);
        return maroon;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MediumAquamarine
    {
      get
      {
        if (mediumAquamarine == null)
          mediumAquamarine = new XPen(XColors.MediumAquamarine, 1, true);
        return mediumAquamarine;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MediumBlue
    {
      get
      {
        if (mediumBlue == null)
          mediumBlue = new XPen(XColors.MediumBlue, 1, true);
        return mediumBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MediumOrchid
    {
      get
      {
        if (mediumOrchid == null)
          mediumOrchid = new XPen(XColors.MediumOrchid, 1, true);
        return mediumOrchid;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MediumPurple
    {
      get
      {
        if (mediumPurple == null)
          mediumPurple = new XPen(XColors.MediumPurple, 1, true);
        return mediumPurple;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MediumSeaGreen
    {
      get
      {
        if (mediumSeaGreen == null)
          mediumSeaGreen = new XPen(XColors.MediumSeaGreen, 1, true);
        return mediumSeaGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MediumSlateBlue
    {
      get
      {
        if (mediumSlateBlue == null)
          mediumSlateBlue = new XPen(XColors.MediumSlateBlue, 1, true);
        return mediumSlateBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MediumSpringGreen
    {
      get
      {
        if (mediumSpringGreen == null)
          mediumSpringGreen = new XPen(XColors.MediumSpringGreen, 1, true);
        return mediumSpringGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MediumTurquoise
    {
      get
      {
        if (mediumTurquoise == null)
          mediumTurquoise = new XPen(XColors.MediumTurquoise, 1, true);
        return mediumTurquoise;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MediumVioletRed
    {
      get
      {
        if (mediumVioletRed == null)
          mediumVioletRed = new XPen(XColors.MediumVioletRed, 1, true);
        return mediumVioletRed;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MidnightBlue
    {
      get
      {
        if (midnightBlue == null)
          midnightBlue = new XPen(XColors.MidnightBlue, 1, true);
        return midnightBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MintCream
    {
      get
      {
        if (mintCream == null)
          mintCream = new XPen(XColors.MintCream, 1, true);
        return mintCream;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen MistyRose
    {
      get
      {
        if (mistyRose == null)
          mistyRose = new XPen(XColors.MistyRose, 1, true);
        return mistyRose;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Moccasin
    {
      get
      {
        if (moccasin == null)
          moccasin = new XPen(XColors.Moccasin, 1, true);
        return moccasin;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen NavajoWhite
    {
      get
      {
        if (navajoWhite == null)
          navajoWhite = new XPen(XColors.NavajoWhite, 1, true);
        return navajoWhite;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Navy
    {
      get
      {
        if (navy == null)
          navy = new XPen(XColors.Navy, 1, true);
        return navy;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen OldLace
    {
      get
      {
        if (oldLace == null)
          oldLace = new XPen(XColors.OldLace, 1, true);
        return oldLace;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Olive
    {
      get
      {
        if (olive == null)
          olive = new XPen(XColors.Olive, 1, true);
        return olive;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen OliveDrab
    {
      get
      {
        if (oliveDrab == null)
          oliveDrab = new XPen(XColors.OliveDrab, 1, true);
        return oliveDrab;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Orange
    {
      get
      {
        if (orange == null)
          orange = new XPen(XColors.Orange, 1, true);
        return orange;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen OrangeRed
    {
      get
      {
        if (orangeRed == null)
          orangeRed = new XPen(XColors.OrangeRed, 1, true);
        return orangeRed;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Orchid
    {
      get
      {
        if (orchid == null)
          orchid = new XPen(XColors.Orchid, 1, true);
        return orchid;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen PaleGoldenrod
    {
      get
      {
        if (paleGoldenrod == null)
          paleGoldenrod = new XPen(XColors.PaleGoldenrod, 1, true);
        return paleGoldenrod;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen PaleGreen
    {
      get
      {
        if (paleGreen == null)
          paleGreen = new XPen(XColors.PaleGreen, 1, true);
        return paleGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen PaleTurquoise
    {
      get
      {
        if (paleTurquoise == null)
          paleTurquoise = new XPen(XColors.PaleTurquoise, 1, true);
        return paleTurquoise;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen PaleVioletRed
    {
      get
      {
        if (paleVioletRed == null)
          paleVioletRed = new XPen(XColors.PaleVioletRed, 1, true);
        return paleVioletRed;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen PapayaWhip
    {
      get
      {
        if (papayaWhip == null)
          papayaWhip = new XPen(XColors.PapayaWhip, 1, true);
        return papayaWhip;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen PeachPuff
    {
      get
      {
        if (peachPuff == null)
          peachPuff = new XPen(XColors.PeachPuff, 1, true);
        return peachPuff;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Peru
    {
      get
      {
        if (peru == null)
          peru = new XPen(XColors.Peru, 1, true);
        return peru;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Pink
    {
      get
      {
        if (pink == null)
          pink = new XPen(XColors.Pink, 1, true);
        return pink;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Plum
    {
      get
      {
        if (plum == null)
          plum = new XPen(XColors.Plum, 1, true);
        return plum;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen PowderBlue
    {
      get
      {
        if (powderBlue == null)
          powderBlue = new XPen(XColors.PowderBlue, 1, true);
        return powderBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Purple
    {
      get
      {
        if (purple == null)
          purple = new XPen(XColors.Purple, 1, true);
        return purple;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Red
    {
      get
      {
        if (red == null)
          red = new XPen(XColors.Red, 1, true);
        return red;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen RosyBrown
    {
      get
      {
        if (rosyBrown == null)
          rosyBrown = new XPen(XColors.RosyBrown, 1, true);
        return rosyBrown;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen RoyalBlue
    {
      get
      {
        if (royalBlue == null)
          royalBlue = new XPen(XColors.RoyalBlue, 1, true);
        return royalBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen SaddleBrown
    {
      get
      {
        if (saddleBrown == null)
          saddleBrown = new XPen(XColors.SaddleBrown, 1, true);
        return saddleBrown;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Salmon
    {
      get
      {
        if (salmon == null)
          salmon = new XPen(XColors.Salmon, 1, true);
        return salmon;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen SandyBrown
    {
      get
      {
        if (sandyBrown == null)
          sandyBrown = new XPen(XColors.SandyBrown, 1, true);
        return sandyBrown;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen SeaGreen
    {
      get
      {
        if (seaGreen == null)
          seaGreen = new XPen(XColors.SeaGreen, 1, true);
        return seaGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen SeaShell
    {
      get
      {
        if (seaShell == null)
          seaShell = new XPen(XColors.SeaShell, 1, true);
        return seaShell;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Sienna
    {
      get
      {
        if (sienna == null)
          sienna = new XPen(XColors.Sienna, 1, true);
        return sienna;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Silver
    {
      get
      {
        if (silver == null)
          silver = new XPen(XColors.Silver, 1, true);
        return silver;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen SkyBlue
    {
      get
      {
        if (skyBlue == null)
          skyBlue = new XPen(XColors.SkyBlue, 1, true);
        return skyBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen SlateBlue
    {
      get
      {
        if (slateBlue == null)
          slateBlue = new XPen(XColors.SlateBlue, 1, true);
        return slateBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen SlateGray
    {
      get
      {
        if (slateGray == null)
          slateGray = new XPen(XColors.SlateGray, 1, true);
        return slateGray;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Snow
    {
      get
      {
        if (snow == null)
          snow = new XPen(XColors.Snow, 1, true);
        return snow;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen SpringGreen
    {
      get
      {
        if (springGreen == null)
          springGreen = new XPen(XColors.SpringGreen, 1, true);
        return springGreen;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen SteelBlue
    {
      get
      {
        if (steelBlue == null)
          steelBlue = new XPen(XColors.SteelBlue, 1, true);
        return steelBlue;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Tan
    {
      get
      {
        if (tan == null)
          tan = new XPen(XColors.Tan, 1, true);
        return tan;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Teal
    {
      get
      {
        if (teal == null)
          teal = new XPen(XColors.Teal, 1, true);
        return teal;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Thistle
    {
      get
      {
        if (thistle == null)
          thistle = new XPen(XColors.Thistle, 1, true);
        return thistle;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Tomato
    {
      get
      {
        if (tomato == null)
          tomato = new XPen(XColors.Tomato, 1, true);
        return tomato;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Transparent
    {
      get
      {
        if (transparent == null)
          transparent = new XPen(XColors.Transparent, 1, true);
        return transparent;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Turquoise
    {
      get
      {
        if (turquoise == null)
          turquoise = new XPen(XColors.Turquoise, 1, true);
        return turquoise;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Violet
    {
      get
      {
        if (violet == null)
          violet = new XPen(XColors.Violet, 1, true);
        return violet;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Wheat
    {
      get
      {
        if (wheat == null)
          wheat = new XPen(XColors.Wheat, 1, true);
        return wheat;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen White
    {
      get
      {
        if (white == null)
          white = new XPen(XColors.White, 1, true);
        return white;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen WhiteSmoke
    {
      get
      {
        if (whiteSmoke == null)
          whiteSmoke = new XPen(XColors.WhiteSmoke, 1, true);
        return whiteSmoke;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen Yellow
    {
      get
      {
        if (yellow == null)
          yellow = new XPen(XColors.Yellow, 1, true);
        return yellow;
      }
    }

    /// <summary>Gets a pre-defined XPen object.</summary>
    public static XPen YellowGreen
    {
      get
      {
        if (yellowGreen == null)
          yellowGreen = new XPen(XColors.YellowGreen, 1, true);
        return yellowGreen;
      }
    }


    static XPen aliceBlue;
    static XPen antiqueWhite;
    static XPen aqua;
    static XPen aquamarine;
    static XPen azure;
    static XPen beige;
    static XPen bisque;
    static XPen black;
    static XPen blanchedAlmond;
    static XPen blue;
    static XPen blueViolet;
    static XPen brown;
    static XPen burlyWood;
    static XPen cadetBlue;
    static XPen chartreuse;
    static XPen chocolate;
    static XPen coral;
    static XPen cornflowerBlue;
    static XPen cornsilk;
    static XPen crimson;
    static XPen cyan;
    static XPen darkBlue;
    static XPen darkCyan;
    static XPen darkGoldenrod;
    static XPen darkGray;
    static XPen darkGreen;
    static XPen darkKhaki;
    static XPen darkMagenta;
    static XPen darkOliveGreen;
    static XPen darkOrange;
    static XPen darkOrchid;
    static XPen darkRed;
    static XPen darkSalmon;
    static XPen darkSeaGreen;
    static XPen darkSlateBlue;
    static XPen darkSlateGray;
    static XPen darkTurquoise;
    static XPen darkViolet;
    static XPen deepPink;
    static XPen deepSkyBlue;
    static XPen dimGray;
    static XPen dodgerBlue;
    static XPen firebrick;
    static XPen floralWhite;
    static XPen forestGreen;
    static XPen fuchsia;
    static XPen gainsboro;
    static XPen ghostWhite;
    static XPen gold;
    static XPen goldenrod;
    static XPen gray;
    static XPen green;
    static XPen greenYellow;
    static XPen honeydew;
    static XPen hotPink;
    static XPen indianRed;
    static XPen indigo;
    static XPen ivory;
    static XPen khaki;
    static XPen lavender;
    static XPen lavenderBlush;
    static XPen lawnGreen;
    static XPen lemonChiffon;
    static XPen lightBlue;
    static XPen lightCoral;
    static XPen lightCyan;
    static XPen lightGoldenrodYellow;
    static XPen lightGray;
    static XPen lightGreen;
    static XPen lightPink;
    static XPen lightSalmon;
    static XPen lightSeaGreen;
    static XPen lightSkyBlue;
    static XPen lightSlateGray;
    static XPen lightSteelBlue;
    static XPen lightYellow;
    static XPen lime;
    static XPen limeGreen;
    static XPen linen;
    static XPen magenta;
    static XPen maroon;
    static XPen mediumAquamarine;
    static XPen mediumBlue;
    static XPen mediumOrchid;
    static XPen mediumPurple;
    static XPen mediumSeaGreen;
    static XPen mediumSlateBlue;
    static XPen mediumSpringGreen;
    static XPen mediumTurquoise;
    static XPen mediumVioletRed;
    static XPen midnightBlue;
    static XPen mintCream;
    static XPen mistyRose;
    static XPen moccasin;
    static XPen navajoWhite;
    static XPen navy;
    static XPen oldLace;
    static XPen olive;
    static XPen oliveDrab;
    static XPen orange;
    static XPen orangeRed;
    static XPen orchid;
    static XPen paleGoldenrod;
    static XPen paleGreen;
    static XPen paleTurquoise;
    static XPen paleVioletRed;
    static XPen papayaWhip;
    static XPen peachPuff;
    static XPen peru;
    static XPen pink;
    static XPen plum;
    static XPen powderBlue;
    static XPen purple;
    static XPen red;
    static XPen rosyBrown;
    static XPen royalBlue;
    static XPen saddleBrown;
    static XPen salmon;
    static XPen sandyBrown;
    static XPen seaGreen;
    static XPen seaShell;
    static XPen sienna;
    static XPen silver;
    static XPen skyBlue;
    static XPen slateBlue;
    static XPen slateGray;
    static XPen snow;
    static XPen springGreen;
    static XPen steelBlue;
    static XPen tan;
    static XPen teal;
    static XPen thistle;
    static XPen tomato;
    static XPen transparent;
    static XPen turquoise;
    static XPen violet;
    static XPen wheat;
    static XPen white;
    static XPen whiteSmoke;
    static XPen yellow;
    static XPen yellowGreen;
  }
}
