using PdfSharp.Drawing;
using BitmapSource = System.Windows.Media.Imaging.BitmapSource;

namespace PdfSharp.Xps.Rendering
{
  class XPImage : XPObject
  {
    public XPImage(BitmapSource bitmapSource)
    {
      xImage = XImage.FromBitmapSource(bitmapSource);
    }

    public XImage XImage
    {
      get { return xImage; }
    }
    XImage xImage;
  }
}
