using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a SolidColorBrush element.
    /// </summary>
    SolidColorBrush ParseSolidColorBrush()
    {
      Debug.Assert(reader.Name == "SolidColorBrush");
      SolidColorBrush brush = new SolidColorBrush();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "Opacity":
            brush.Opacity = ParseDouble(reader.Value);
            break;

          case "Color":
            brush.Color = Color.Parse(reader.Value);
            break;

          case "x:Key":
            brush.Key = reader.Value;
            break;
        }
      }
      MoveBeyondThisElement();
      return brush;
    }
  }
}