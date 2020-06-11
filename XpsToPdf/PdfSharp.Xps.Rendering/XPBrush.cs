using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Rendering
{
  class XPBrush : XPObject
  {
    protected XPBrush(Brush brush)
    {
      this.brush = brush;
    }

    Brush Brush => brush;
    Brush brush;
  }
}
