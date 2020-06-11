using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a DocumentOutline element.
    /// </summary>
    DocumentOutline ParseDocumentOutline()
    {
      Debug.Assert(reader.Name == "DocumentOutline");
      bool isEmptyElement = reader.IsEmptyElement;
      DocumentOutline documentOutline = new DocumentOutline();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "xml:lang":
            documentOutline.lang = reader.Value;
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return documentOutline;
    }
  }
}