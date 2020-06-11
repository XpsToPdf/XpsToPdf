using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a RadialGradientBrush element.
    /// </summary>
    RadialGradientBrush ParseRadialGradientBrush()
    {
      Debug.Assert(reader.Name == "RadialGradientBrush");
      bool isEmptyElement = reader.IsEmptyElement;
      RadialGradientBrush brush = new RadialGradientBrush();
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
            brush.ColorInterpolationMode= ParseEnum<ClrIntMode>(reader.Value);
            break;

          case "SpreadMethod":
            brush.SpreadMethod = ParseEnum<SpreadMethod>(reader.Value);
            break;

          case "MappingMode ":
            brush.MappingMode = ParseEnum<MappingMode>(reader.Value);
            break;

          case "Center":
            brush.Center = Point.Parse(reader.Value);
            break;

          case "GradientOrigin":
            brush.GradientOrigin = Point.Parse(reader.Value);
            break;

          case "RadiusX":
            brush.RadiusX =ParseDouble(reader.Value);
            break;

          case "RadiusY":
            brush.RadiusY = ParseDouble(reader.Value);
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
            case "RadialGradientBrush.Transform":
              MoveToNextElement();
              brush.Transform = ParseMatrixTransform();
              MoveToNextElement();
              break;

            case "RadialGradientBrush.GradientStops":
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