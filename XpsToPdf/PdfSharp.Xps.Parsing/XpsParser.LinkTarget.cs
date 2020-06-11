using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a LinkTarget element.
    /// </summary>
    LinkTarget ParseLinkTarget()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      LinkTarget linkTarget = new LinkTarget();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "Name":
            linkTarget.Name = reader.Value;
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return linkTarget;
    }
  }
}