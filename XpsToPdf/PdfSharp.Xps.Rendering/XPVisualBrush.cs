using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Rendering
{
  class XPVisualBrush : XPTilingBrush
  {
    public XPVisualBrush(ImageBrush brush)
      : base(brush)
    {
    }
  }
}
