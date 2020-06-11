using System;
using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a Glyphs element.
    /// </summary>
    Glyphs ParseGlyphs()
    {
      Debug.Assert(reader.Name == "Glyphs");
      Glyphs glyphs = new Glyphs();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "BidiLevel":
            glyphs.BidiLevel = Int32.Parse(reader.Value);
            break;

          case "CaretStops":
            glyphs.CaretStops = reader.Value;
            break;

          case "DeviceFontName":
            glyphs.DeviceFontName = reader.Value;
            break;

          case "Fill":
            glyphs.Fill = ParseBrush(reader.Value); 
            break;

          case "FontRenderingEmSize":
            glyphs.FontRenderingEmSize = ParseDouble(reader.Value);
            break;

          case "FontUri":
            glyphs.FontUri = reader.Value;
            break;

          case "OriginX":
            glyphs.OriginX = ParseDouble(reader.Value);
            break;

          case "OriginY":
            glyphs.OriginY = ParseDouble(reader.Value);
            break;

          case "IsSideways":
            glyphs.IsSideways = ParseBool(reader.Value);
            break;

          case "Indices":
            glyphs.Indices = new GlyphIndices(reader.Value);
            break;

          case "UnicodeString":
            glyphs.UnicodeString = reader.Value;
            break;

          case "StyleSimulations":
            glyphs.StyleSimulations = ParseEnum<StyleSimulations>(reader.Value);
            break;

          case "RenderTransform":
            glyphs.RenderTransform = ParseMatrixTransform(reader.Value);
            break;

          case "Clip":
            glyphs.Clip = ParsePathGeometry(reader.Value);
            break;

          case "Opacity":
            glyphs.Opacity = ParseDouble(reader.Value);
            break;

          case "Name":
            glyphs.Name = reader.Value;
            break;

          case "FixedPage.NavigateUri":
            glyphs.FixedPage_NavigateUri = reader.Value;
            break;

          case "xml:lang":
            glyphs.lang = reader.Value;
            break;

          case "x:Key":
            glyphs.Key = reader.Value;
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      // TODO Glyphs
      MoveBeyondThisElement();
      return glyphs;
    }
  }
}