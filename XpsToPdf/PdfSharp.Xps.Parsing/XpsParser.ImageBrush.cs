using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses an ImageBrush element.
    /// </summary>
    ImageBrush ParseImageBrush()
    {
      Debug.Assert(reader.Name == "ImageBrush");
      bool isEmptyElement = reader.IsEmptyElement;
      ImageBrush brush = new ImageBrush();
      brush.Opacity = 1;
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          //case "Name":
          //  //brush.Name = this.rdr.Value;
          //  break;

          case "Opacity":
            brush.Opacity = ParseDouble(reader.Value);
            break;

          case "Transform":
            brush.Transform = ParseMatrixTransform(reader.Value);
            break;

          case "ViewboxUnits":
            brush.ViewboxUnits = ParseEnum<ViewUnits>(reader.Value);
            break;

          case "ViewportUnits":
            brush.ViewportUnits = ParseEnum<ViewUnits>(reader.Value);
            break;

          case "TileMode":
            brush.TileMode = ParseEnum<TileMode>(reader.Value);
            break;

          case "Viewbox":
            brush.Viewbox = Rect.Parse(reader.Value);
            break;

          case "Viewport":
            brush.Viewport = Rect.Parse(reader.Value);
            break;

          case "ImageSource":
            brush.ImageSource = reader.Value;
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
            case "ImageBrush.Transform":
              MoveToNextElement();
              brush.Transform = ParseMatrixTransform();
              MoveToNextElement();
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