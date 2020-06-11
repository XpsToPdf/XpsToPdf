using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a PolyQuadraticBezierSegment element.
    /// </summary>
    PolyQuadraticBezierSegment ParsePolyQuadraticBezierSegment()
    {
      Debug.Assert(reader.Name == "PolyQuadraticBezierSegment");
      PolyQuadraticBezierSegment seg = new PolyQuadraticBezierSegment();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "IsStroked":
            seg.IsStroked = ParseBool(reader.Value);
            break;

          case "Points":
            seg.Points = Point.ParsePoints(reader.Value);
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveBeyondThisElement();
      return seg;
    }
  }
}