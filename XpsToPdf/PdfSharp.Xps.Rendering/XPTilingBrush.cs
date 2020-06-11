using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Rendering
{
  abstract class XPTilingBrush : XPBrush
  {
    protected XPTilingBrush(Brush brush)
      : base(brush)
    {
    }
  }
}
