using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a GradientStop element.
    /// </summary>
    GradientStop ParseGradientStop()
    {
      GradientStop gs = new GradientStop();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "Color":
            gs.Color = Color.Parse(reader.Value);
            break;

          case "Offset":
            gs.Offset = ParseDouble(reader.Value);
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveBeyondThisElement();
      return gs;
    }
  }
}