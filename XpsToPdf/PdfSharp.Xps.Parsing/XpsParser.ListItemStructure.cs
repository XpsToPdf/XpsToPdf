using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a ListItemStructure element.
    /// </summary>
    ListItemStructure ParseListItemStructure()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      ListItemStructure listItemStructure = new ListItemStructure();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "Marker":
            listItemStructure.Marker = reader.Value;
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return listItemStructure;
    }
  }
}