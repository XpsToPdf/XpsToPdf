using PdfSharp.Xps.XpsModel;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Drawing;

namespace PdfSharp.Xps.Rendering
{
  /// <summary>
  /// Implements the rendering process.
  /// </summary>
  class PdfRenderer
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PdfRenderer"/> class.
    /// </summary>
    internal PdfRenderer()
    {
      //this.document = new PdfDocument();
      //this.page = this.document.AddPage();
      //this.context = context;
    }

    internal PdfPage CreatePage(PdfDocument doc, FixedPage fixedPage)
    {
      PdfPage page = doc.Pages.Add();
      page.Width = XUnit.FromPresentation(fixedPage.Width);
      page.Height = XUnit.FromPresentation(fixedPage.Height);
      return page;
    }

    internal void RenderPage(PdfPage page, FixedPage fixedPage)
    {
      this.page = page;

      context = new DocumentRenderingContext(page.Owner);

      //this.page.Width = fixedPage.Width;
      //this.page.Height = fixedPage.Height;

      //this.gsStack = new GraphicsStateStack(this);
      PdfContent content = null;
      //switch (options)
      //{
      //  case XGraphicsPdfPageOptions.Replace:
      //    page.Contents.Elements.Clear();
      //    goto case XGraphicsPdfPageOptions.Append;

      //  case XGraphicsPdfPageOptions.Prepend:
      //    content = page.Contents.PrependContent();
      //    break;

      //  case XGraphicsPdfPageOptions.Append:
      content = page.Contents.AppendContent();
      //    break;
      //}
      page.RenderContent = content;

      writer = new PdfContentWriter(context, this.page);

      //Initialize();

      writer.BeginContent(false);
      writer.WriteElements(fixedPage.Content);
      writer.EndContent();
    }

    internal PdfDocument Document => page.document;

    PdfPage page;
    PdfContentWriter writer;
    DocumentRenderingContext context;
  }
}