using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a PathFigure element.
    /// </summary>
    PathFigure ParsePathFigure()
    {
      Debug.Assert(reader.Name == "PathFigure");
      bool isEmptyElement = reader.IsEmptyElement;
      PathFigure fig = new PathFigure();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "IsClosed":
            fig.IsClosed = ParseBool(reader.Value);
            break;

          case "StartPoint":
            fig.StartPoint = Point.Parse(reader.Value);
            break;

          case "IsFilled":
            fig.IsFilled = ParseBool(reader.Value);
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      if (!isEmptyElement)
      {
        MoveToNextElement();
        while (reader.IsStartElement())
        {
          switch (reader.Name)
          {
            case "PolyLineSegment":
              fig.Segments.Add(ParsePolyLineSegment());
              break;

            case "PolyBezierSegment":
              fig.Segments.Add(ParsePolyBezierSegment());
              break;

            case "ArcSegment":
              fig.Segments.Add(ParseArcSegment());
              break;

            case "PolyQuadraticBezierSegment":
              fig.Segments.Add(ParsePolyQuadraticBezierSegment());
              break;

            default:
              Debugger.Break();
              break;
          }
        }
      }
      MoveToNextElement();
      return fig;
    }
  }
}