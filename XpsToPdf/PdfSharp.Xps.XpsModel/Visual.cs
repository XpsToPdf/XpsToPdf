namespace PdfSharp.Xps.XpsModel
{
  /// <summary>
  /// Represents the content of a VisualBrush.
  /// </summary>
  class Visual : XpsElement
  {
    /// <summary>
    /// A collection of Path, Glyphs, and Canvas objects.
    /// </summary>
    public XpsElementCollection Content = new XpsElementCollection();
  }
}