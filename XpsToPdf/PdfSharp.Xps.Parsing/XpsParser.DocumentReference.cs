using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a DocumentReference element.
    /// </summary>
    DocumentReference ParseDocumentReference()
    {
      Debug.Assert(reader.Name == "DocumentReference");
      bool isEmptyElement = reader.IsEmptyElement;
      DocumentReference documentReference = new DocumentReference();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "Source":
            documentReference.Source = reader.Value;
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return documentReference;
    }
  }
}