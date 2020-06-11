using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Rendering
{
  class XPLinearGradientBrush : XPGradientBrush
  {
    protected XPLinearGradientBrush(LinearGradientBrush brush)
      : base(brush)
    {
    }
  }
}
