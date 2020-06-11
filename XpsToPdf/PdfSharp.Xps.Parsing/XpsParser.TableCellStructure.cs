using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a TableCellStructure element.
    /// </summary>
    TableCellStructure ParseTableCellStructure()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      TableCellStructure tableCellStructure = new TableCellStructure();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "RowSpan":
            tableCellStructure.RowSpan = int.Parse(reader.Value);
            break;

          case "ColumnSpan":
            tableCellStructure.ColumnSpan = int.Parse(reader.Value);
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return tableCellStructure;
    }
  }
}