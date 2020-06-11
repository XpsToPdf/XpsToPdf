using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a element.
    /// </summary>
    Canvas ParseObject()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      Canvas canvas = new Canvas();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "Name":
            //canvas.Name = this.reader.Value;
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
            case "Canvas.Resources":
              //MoveToNextElement();
              //canvas.Resources = ParseResourceDictionary();
              break;

            default:
              Debugger.Break();
              break;
          }
        }
      }
      MoveToNextElement();
      return canvas;
    }
  }
}