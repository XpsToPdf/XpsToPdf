using System.Diagnostics;
using PdfSharp.Xps.XpsModel;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Drawing;

namespace PdfSharp.Xps.Rendering
{
  class XPImageBrush : XPTilingBrush
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="XPObjectBase"/> class.
    /// </summary>
    XPImageBrush(ImageBrush brush)
      : base(brush)
    {
    }

    public static XPImageBrush Create(DocumentRenderingContext context, ImageBrush brush)
    {
      XPImageBrush xpImageBrush = new XPImageBrush(brush);
      xpImageBrush.Construct(context, brush);
      return xpImageBrush;
    }

    void Construct(DocumentRenderingContext context, ImageBrush brush)
    {
      FixedPage fpage = brush.GetParent<FixedPage>();
      if (fpage == null)
        Debug.Assert(false);

      FixedPayload payload = fpage.Document.Payload;  // TODO: find better way to get FixedPayload
      //Debug.Assert(Object.Equals(payload, Context.


      form = XFormBuilder.FromImageBrush(context, brush);

      // Get the font object.
      // Important: font.PdfFont is not yet defined here on the first call
      //string uriString = brush.ImageSource;
      //BitmapSource bitmapSource = payload.GetImage(uriString);

      //XPImage xpImage = new XPImage(bitmapSource);
    }

    XForm Form => form;
    XForm form;

    PdfFormXObject FormXObject => formXObject;
    PdfFormXObject formXObject = null;

    PdfTilingPattern Pattern => pattern;
    PdfTilingPattern pattern = null;
  }
}
