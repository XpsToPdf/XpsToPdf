using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a SignatureDefinition element.
    /// </summary>
    SignatureDefinition ParseSignatureDefinition()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      SignatureDefinition signatureDefinition = new SignatureDefinition();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "SpotID":
            signatureDefinition.SpotID = reader.Value;
            break;

          case "SignerName":
            signatureDefinition.SignerName = reader.Value;
            break;

          case "xml:lang":
            signatureDefinition.lang = reader.Value;
            break;
          
          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return signatureDefinition;
    }
  }
}