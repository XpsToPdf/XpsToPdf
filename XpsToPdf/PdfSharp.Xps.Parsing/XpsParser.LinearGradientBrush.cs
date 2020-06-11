using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a LinearGradientBrush element.
    /// </summary>
    LinearGradientBrush ParseLinearGradientBrush()
    {
      Debug.Assert(reader.Name == "LinearGradientBrush");
      bool isEmptyElement = reader.IsEmptyElement;
      LinearGradientBrush brush = new LinearGradientBrush();
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

          case "ColorInterpolationMode":
            brush.ColorInterpolationMode = ParseEnum<ClrIntMode>(reader.Value);
            break;

          case "SpreadMethod":
            brush.SpreadMethod = ParseEnum<SpreadMethod>(reader.Value);
            break;

          case "MappingMode":
            brush.MappingMode = ParseEnum<MappingMode>(reader.Value);
            break;

          case "StartPoint":
            brush.StartPoint = Point.Parse(reader.Value);
            break;

          case "EndPoint":
            brush.EndPoint = Point.Parse(reader.Value);
            break;

          case "x:Key":
            brush.Key = reader.Value;
            break;

          default:
            Debugger.Break();
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
            case "LinearGradientBrush.Transform":
              MoveToNextElement();
              brush.Transform = ParseMatrixTransform();
              MoveToNextElement();
              break;

            case "LinearGradientBrush.GradientStops":
              // do not MoveToNextElement();
              brush.GradientStops = ParseGradientStops();
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