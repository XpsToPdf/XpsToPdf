using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a ResourceDictionary element.
    /// </summary>
    void ParseResourceDictionary(ResourceDictionary dict)
    {
      //Debug.Assert(this.reader.Name == "ResourceDictionary");
      Debug.Assert(reader.Name.Contains("Resource"));
      try
      {
        bool isEmptyElement = reader.IsEmptyElement;
        //ResourceDictionary dict = new ResourceDictionary();
        while (MoveToNextAttribute())
        {
          switch (reader.Name)
          {
            case "Source":
              dict.Source = reader.Value;
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
              case "ImageBrush":
                ImageBrush ibrush = ParseImageBrush();
                dict.elements[ibrush.Key] = ibrush;
                ibrush.Parent = dict;
                break;

              case "LinearGradientBrush":
                LinearGradientBrush lbrush = ParseLinearGradientBrush();
                dict.elements[lbrush.Key] = lbrush;
                lbrush.Parent = dict;
                break;

              case "RadialGradientBrush":
                RadialGradientBrush rbrush = ParseRadialGradientBrush();
                dict.elements[rbrush.Key] = rbrush;
                rbrush.Parent = dict;
                break;

              case "VisualBrush":
                VisualBrush vbrush = ParseVisualBrush();
                dict.elements[vbrush.Key] = vbrush;
                vbrush.Parent = dict;
                break;

              case "SolidColorBrush":
                VisualBrush sbrush = ParseVisualBrush();
                dict.elements[sbrush.Key] = sbrush;
                sbrush.Parent = dict;
                break;

              case "MatrixTransform":
                MatrixTransform transform = ParseMatrixTransform();
                dict.elements[transform.Key] = transform;
                transform.Parent = dict;
                break;

              case "PathGeometry":
                PathGeometry geo = ParsePathGeometry();
                dict.elements[geo.Key] = geo;
                geo.Parent = dict;
                break;

              case "Path":
                Path path = ParsePath();
                dict.elements[path.Key] = path;
                path.Parent = dict;
                break;

              case "Glyphs":
                Glyphs glyphs = ParseGlyphs();
                dict.elements[glyphs.Key] = glyphs;
                glyphs.Parent = dict;
                break;

              case "Canvas":
                Canvas canvas = ParseCanvas();
                dict.elements[canvas.Key] = canvas;
                canvas.Parent = dict;
                break;

              default:
                Debugger.Break();
                break;
            }
          }
        }
        MoveToNextElement();
        //return dict;
      }
      finally
      {
      }
    }
  }
}