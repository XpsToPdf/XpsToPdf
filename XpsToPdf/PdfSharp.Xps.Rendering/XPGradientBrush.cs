using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Rendering
{
  abstract class XPGradientBrush : XPBrush
  {
    protected XPGradientBrush(Brush brush)
      : base(brush)
    {
    }
  }
}
