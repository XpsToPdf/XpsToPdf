using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a Visual element.
    /// </summary>
    Visual ParseVisual()
    {
      bool isEmptyElement = reader.IsEmptyElement;
      Visual visual = new Visual();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
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
          XpsElement element = null;
          switch (reader.Name)
          {
            case "Canvas":
              element = ParseCanvas();
              visual.Content.Add(element);
              element.Parent = visual;
              break;

            case "Path":
              element = ParsePath();
              visual.Content.Add(element);
              element.Parent = visual;
              break;

            case "Glyphs":
              element = ParseGlyphs();
              visual.Content.Add(element);
              element.Parent = visual;
              break;

            default:
              Debugger.Break();
              break;
          }
        }
      }
      MoveToNextElement();
      return visual;
    }
  }
}