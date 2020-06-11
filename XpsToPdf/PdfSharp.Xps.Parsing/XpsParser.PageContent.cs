using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a PageContent element.
    /// </summary>
    PageContent ParsePageContent()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      PageContent pageContent = new PageContent();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "Width":
            pageContent.Width = int.Parse(reader.Value);
            break;

          case "Height":
            pageContent.Height = int.Parse(reader.Value);
            break;

          case "LinkTargets":
            pageContent.LinkTargets = int.Parse(reader.Value);
            break;

          case "Source":
            pageContent.Source = reader.Value;
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return pageContent;
    }
  }
}