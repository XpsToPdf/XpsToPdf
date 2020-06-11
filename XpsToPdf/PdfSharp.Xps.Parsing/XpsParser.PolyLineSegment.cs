using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a PolyLineSegment element.
    /// </summary>
    PolyLineSegment ParsePolyLineSegment()
    {
      Debug.Assert(reader.Name == "PolyLineSegment");
      PolyLineSegment seg = new PolyLineSegment();
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