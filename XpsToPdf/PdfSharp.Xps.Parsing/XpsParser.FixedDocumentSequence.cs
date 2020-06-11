using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a FixedDocumentSequence element.
    /// </summary>
    FixedDocumentSequence ParseFixedDocumentSequence()
    {
      Debug.Assert(reader.Name == "FixedDocumentSequence");
      bool isEmptyElement = reader.IsEmptyElement;
      FixedDocumentSequence fdseq = new FixedDocumentSequence();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          default:
            //UnexpectedAttribute();
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
            case "DocumentReference":
              {
                DocumentReference dref = ParseDocumentReference();
                //Debug.WriteLine("Path: " + (path.Name != null ? path.Name : ""));
                fdseq.DocumentReferences.Add(dref);
              }
              break;

            default:
              Debugger.Break();
              break;
          }
        }
      }
      MoveToNextElement();
      return fdseq;
    }
  }
}