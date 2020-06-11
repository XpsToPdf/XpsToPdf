using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a FixedDocument element.
    /// </summary>
    FixedDocument ParseFixedDocument()
    {
      Debug.Assert(reader.Name == "FixedDocument");
      bool isEmptyElement = reader.IsEmptyElement;
      FixedDocument fdoc = new FixedDocument();
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
            case "PageContent":
              {
                isEmptyElement = reader.IsEmptyElement;
                while (MoveToNextAttribute())
                {
                  switch (reader.Name)
                  {
                    case "Source":
                      fdoc.PageContentUriStrings.Add(reader.Value);
                      break;

                    case "Width":
                      // TODO: preview width
                      break;

                    case "Height":
                      // TODO: preview height
                      break;

                    default:
                      UnexpectedAttribute(reader.Name);
                      break;
                  }
                }
                if (!isEmptyElement)
                {
                  MoveToNextElement();
                  // Move beyond PageContent.LinkTargets
                  if (reader.IsStartElement())
                    MoveBeyondThisElement();
                }
              }
              MoveToNextElement();
              break;

            case "LinkTarget":
              Debug.Assert(false);
              MoveBeyondThisElement();
              break;

            default:
              Debugger.Break();
              break;
          }
        }
      }
      MoveToNextElement();
      return fdoc;
    }
  }
}