using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a OutlineEntry element.
    /// </summary>
    OutlineEntry ParseOutlineEntry()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      OutlineEntry outlineEntry = new OutlineEntry();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "OutlineLevel":
            outlineEntry.OutlineLevel = int.Parse(reader.Value);
            break;

          case "OutlineTarget":
            outlineEntry.OutlineTarget = reader.Value;
            break;

          case "Description":
            outlineEntry.Description = reader.Value;
            break;

          case "xml:lang":
            outlineEntry.lang = reader.Value;
            break;
          
          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return outlineEntry;
    }
  }
}