using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Rendering
{
  class XPRadialGradientBrush : XPGradientBrush
  {
    protected XPRadialGradientBrush(RadialGradientBrush brush)
      : base(brush)
    {
    }
  }
}
