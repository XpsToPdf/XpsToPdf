using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a DocumentStructure element.
    /// </summary>
    DocumentStructure ParseDocumentStructure()
    {
      Debug.Assert(reader.Name == "DocumentStructure");
      bool isEmptyElement = reader.IsEmptyElement;
      DocumentStructure documentStructure = new DocumentStructure();
      if (!isEmptyElement)
      {
        MoveToNextElement();
        while (reader.IsStartElement())
        {
          switch (reader.Name)
          {
            case "Outline":
              documentStructure.Outline = ParseDocumentOutline();
              break;

            default:
              Debugger.Break();
              break;
          }
        }
      }
      MoveToNextElement();
      return documentStructure;
    }
  }
}