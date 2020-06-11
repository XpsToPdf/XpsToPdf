using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a VisualBrush element.
    /// </summary>
    VisualBrush ParseVisualBrush()
    {
      bool isEmptyElement = reader.IsEmptyElement;
      VisualBrush brush = new VisualBrush();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "Opacity":
            brush.Opacity = ParseDouble(reader.Value);
            break;

          case "Transform":
            brush.Transform = ParseMatrixTransform(reader.Value);
            break;

          case "Viewbox":
            brush.Viewbox = Rect.Parse(reader.Value);
            break;

          case "Viewport":
            brush.Viewport = Rect.Parse(reader.Value);
            break;

          case "TileMode":
            brush.TileMode = ParseEnum<TileMode>(reader.Value);
            break;

          case "ViewboxUnits":
            brush.ViewboxUnits = ParseEnum<ViewUnits>(reader.Value);
            break;

          case "ViewportUnits":
            brush.ViewportUnits = ParseEnum<ViewUnits>(reader.Value);
            break;

          case "Visual":
            brush.Visual = ParseVisual();
            break;

          case "x:Key":
            brush.Key = reader.Value;
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
            case "VisualBrush.Transform":
              MoveToNextElement();
              brush.Transform = ParseMatrixTransform();
              break;

            case "VisualBrush.GradientStops":
              MoveToNextElement();
              //brush.GradientStops= ParseGradientStops();
              break;

            case "VisualBrush.Visual":
              //MoveToNextElement();
              brush.Visual = ParseVisual();
              brush.Visual.Parent = brush;
              break;

            default:
              Debugger.Break();
              break;
          }
        }
      }
      MoveToNextElement();
      return brush;
    }
  }
}