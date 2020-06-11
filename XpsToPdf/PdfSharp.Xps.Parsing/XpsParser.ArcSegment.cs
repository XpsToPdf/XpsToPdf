using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a PolyBezierSegment element.
    /// </summary>
    ArcSegment ParseArcSegment()
    {
      Debug.Assert(reader.Name == "ArcSegment");
      ArcSegment seg = new ArcSegment();
      seg.IsStroked = true;
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "Point":
            seg.Point = Point.Parse(reader.Value);
            break;

          case "Size":
            seg.Size = Size.Parse(reader.Value);
            break;

          case "RotationAngle":
            seg.RotationAngle = ParseDouble(reader.Value);
            break;

          case "IsLargeArc":
            seg.IsLargeArc = ParseBool(reader.Value);
            break;

          case "SweepDirection":
            seg.SweepDirection = ParseEnum<SweepDirection>(reader.Value);
            break;

          case "IsStroked":
            seg.IsStroked = ParseBool(reader.Value);
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