using PdfSharp.Pdf;

namespace PdfSharp.Xps.Rendering
{
  /// <summary>
  /// Keeps track of the already created PDF objects.
  /// </summary>
  class DocumentRenderingContext
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentRenderingContext"/> class.
    /// </summary>
    public DocumentRenderingContext(PdfDocument pdfDocument) //, XpsDocument xpsDocument)
    {
      this.pdfDocument = pdfDocument;
      //this.xpsDocument = xpsDocument;
    }

    /// <summary>
    /// Gets the PDF document.
    /// </summary>
    public PdfDocument PdfDocument => pdfDocument;

    PdfDocument pdfDocument;

    ///// <summary>
    ///// Gets the XPS document.
    ///// </summary>
    //XpsDocument XpsDocument
    //{
    //  get { return this.xpsDocument; }
    //}
    //XpsDocument xpsDocument;
  }
}
