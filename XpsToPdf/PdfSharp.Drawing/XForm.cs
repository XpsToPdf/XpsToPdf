#region PDFsharp - A .NET library for processing PDF
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//
// Copyright (c) 2005-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Diagnostics;
using System.IO;
#if GDI
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
#endif
#if WPF
#endif
using PdfSharp.Drawing.Pdf;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Represents a graphical object that can be used to render retained graphics on it.
  /// In GDI+ it is represented by a Metafile, in WPF by a DrawingVisual, and in PDF by a Form XObjects.
  /// </summary>
  public class XForm : XImage, IContentStream
  {
    internal enum FormState
    {
      /// <summary>
      /// The form is an imported PDF page.
      /// </summary>
      NotATemplate,

      /// <summary>
      /// The template is just created.
      /// </summary>
      Created,

      /// <summary>
      /// XGraphics.FromForm() was called.
      /// </summary>
      UnderConstruction,

      /// <summary>
      /// The form was drawn at least once and is 'frozen' now.
      /// </summary>
      Finished,
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XForm"/> class.
    /// </summary>
    protected XForm()
    { }

#if GDI
    /// <summary>
    /// Initializes a new instance of the XForm class such that it can be drawn on the specified graphics
    /// object.
    /// </summary>
    /// <param name="gfx">The graphics object that later is used to draw this form.</param>
    /// <param name="size">The size in points of the form.</param>
    public XForm(XGraphics gfx, XSize size)
    {
      if (gfx == null)
        throw new ArgumentNullException("gfx");
      if (size.width < 1 || size.height < 1)
        throw new ArgumentNullException("size", "The size of the XPdfForm is to small.");

      this.formState = FormState.Created;
      //this.templateSize = size;
      this.viewBox.width = size.width;
      this.viewBox.height = size.height;

      // If gfx belongs to a PdfPage also create the PdfFormXObject
      if (gfx.PdfPage != null)
      {
        this.document = gfx.PdfPage.Owner;
        this.pdfForm = new PdfFormXObject(this.document, this);
        PdfRectangle rect = new PdfRectangle(new XPoint(), size);
        this.pdfForm.Elements.SetRectangle(PdfFormXObject.Keys.BBox, rect);
      }
    }
#endif

#if GDI
    /// <summary>
    /// Initializes a new instance of the XForm class such that it can be drawn on the specified graphics
    /// object.
    /// </summary>
    /// <param name="gfx">The graphics object that later is used to draw this form.</param>
    /// <param name="width">The width of the form.</param>
    /// <param name="height">The height of the form.</param>
    public XForm(XGraphics gfx, XUnit width, XUnit height)
      : this(gfx, new XSize(width, height))
    { }
#endif

    /// <summary>
    /// Initializes a new instance of the <see cref="XForm"/> class that represents a page of a PDF document.
    /// </summary>
    /// <param name="document">The PDF document.</param>
    /// <param name="viewBox">The view box of the page.</param>
    public XForm(PdfDocument document, XRect viewBox)
    {
      if (viewBox.width < 1 || viewBox.height < 1)
        throw new ArgumentNullException("viewBox", "The size of the XPdfForm is to small.");
      // I must tie the XPdfForm to a document immediately, because otherwise I would have no place where
      // to store the resources.
      if (document == null)
        throw new ArgumentNullException("document", "An XPdfForm template must be associated with a document at creation time.");

      formState = FormState.Created;
      this.document = document;
      pdfForm = new PdfFormXObject(document, this);
      //this.templateSize = size;
      this.viewBox = viewBox;
      PdfRectangle rect = new PdfRectangle(viewBox);
      pdfForm.Elements.SetRectangle(PdfFormXObject.Keys.BBox, rect);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XForm"/> class that represents a page of a PDF document.
    /// </summary>
    /// <param name="document">The PDF document.</param>
    /// <param name="size">The size of the page.</param>
    public XForm(PdfDocument document, XSize size)
      : this(document, new XRect(0, 0, size.width, size.height))
    {
      ////if (size.width < 1 || size.height < 1)
      ////  throw new ArgumentNullException("size", "The size of the XPdfForm is to small.");
      ////// I must tie the XPdfForm to a document immediately, because otherwise I would have no place where
      ////// to store the resources.
      ////if (document == null)
      ////  throw new ArgumentNullException("document", "An XPdfForm template must be associated with a document.");

      ////this.formState = FormState.Created;
      ////this.document = document;
      ////this.pdfForm = new PdfFormXObject(document, this);
      ////this.templateSize = size;
      ////PdfRectangle rect = new PdfRectangle(new XPoint(), size);
      ////this.pdfForm.Elements.SetRectangle(PdfFormXObject.Keys.BBox, rect);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XForm"/> class that represents a page of a PDF document.
    /// </summary>
    /// <param name="document">The PDF document.</param>
    /// <param name="width">The width of the page.</param>
    /// <param name="height">The height of the page</param>
    public XForm(PdfDocument document, XUnit width, XUnit height)
      : this(document, new XRect(0, 0, width, height))
    { }

    /// <summary>
    /// This function should be called when drawing the content of this form is finished.
    /// The XGraphics object used for drawing the content is disposed by this function and 
    /// cannot be used for any further drawing operations.
    /// PDFsharp automatically calls this function when this form was used the first time
    /// in a DrawImage function. 
    /// </summary>
    public void DrawingFinished()
    {
      if (formState == FormState.Finished)
        return;

      if (formState == FormState.NotATemplate)
        throw new InvalidOperationException("This object is an imported PDF page and you cannot finish drawing on it because you must not draw on it at all.");

      Finish();
    }

    /// <summary>
    /// Called from XGraphics constructor that creates an instance that work on this form.
    /// </summary>
    internal void AssociateGraphics(XGraphics gfx)
    {
      if (formState == FormState.NotATemplate)
        throw new NotImplementedException("The current version of PDFsharp cannot draw on an imported page.");

      if (formState == FormState.UnderConstruction)
        throw new InvalidOperationException("An XGraphics object already exists for this form.");

      if (formState == FormState.Finished)
        throw new InvalidOperationException("After drawing a form it cannot be modified anymore.");

      Debug.Assert(formState == FormState.Created);
      formState = FormState.UnderConstruction;
      this.gfx = gfx;
    }
    internal XGraphics gfx;

    /// <summary>
    /// Disposes this instance.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
    }

    /// <summary>
    /// Sets the form in the state FormState.Finished.
    /// </summary>
    internal virtual void Finish()
    {
#if GDI
      if (this.formState == FormState.NotATemplate || this.formState == FormState.Finished)
        return;

      if (this.gfx.metafile != null)
        this.gdiImage = this.gfx.metafile;

      Debug.Assert(this.formState == FormState.Created || this.formState == FormState.UnderConstruction);
      this.formState = FormState.Finished;
      this.gfx.Dispose();
      this.gfx = null;

      if (this.pdfRenderer != null)
      {
        //this.pdfForm.CreateStream(PdfEncoders.RawEncoding.GetBytes(this.pdfRenderer.GetContent()));
        this.pdfRenderer.Close();
        Debug.Assert(this.pdfRenderer == null);

        if (this.document.Options.CompressContentStreams)
        {
          this.pdfForm.Stream.Value = Filtering.FlateDecode.Encode(this.pdfForm.Stream.Value);
          this.pdfForm.Elements["/Filter"] = new PdfName("/FlateDecode");
        }
        int length = this.pdfForm.Stream.Length;
        this.pdfForm.Elements.SetInteger("/Length", length);
      }
#endif
#if WPF
#endif
    }

    /// <summary>
    /// Gets the owning document.
    /// </summary>
    internal PdfDocument Owner => document;

    PdfDocument document;

    /// <summary>
    /// Gets the color model used in the underlying PDF document.
    /// </summary>
    internal PdfColorMode ColorMode
    {
      get
      {
        if (document == null)
          return PdfColorMode.Undefined;
        return document.Options.ColorMode;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is a template.
    /// </summary>
    internal bool IsTemplate => formState != FormState.NotATemplate;

    internal FormState formState;

    /// <summary>
    /// Get the width of the page identified by the property PageNumber.
    /// </summary>
    [Obsolete("Use either PixelWidth or PointWidth. Temporarily obsolete because of rearrangements for WPF. Currently same as PixelWidth, but will become PointWidth in future releases of PDFsharp.")]
    public override double Width =>
        //get { return this.templateSize.width; }
        viewBox.Width;

    /// <summary>
    /// Get the width of the page identified by the property PageNumber.
    /// </summary>
    [Obsolete("Use either PixelHeight or PointHeight. Temporarily obsolete because of rearrangements for WPF. Currently same as PixelHeight, but will become PointHeight in future releases of PDFsharp.")]
    public override double Height =>
        //get { return this.templateSize.height; }
        viewBox.height;

    /// <summary>
    /// Get the width in point of this image.
    /// </summary>
    public override double PointWidth =>
        //get { return this.templateSize.width; }
        viewBox.width;

    /// <summary>
    /// Get the height in point of this image.
    /// </summary>
    public override double PointHeight =>
        //get { return this.templateSize.height; }
        viewBox.height;

    /// <summary>
    /// Get the width of the page identified by the property PageNumber.
    /// </summary>
    public override int PixelWidth =>
        //get { return (int)this.templateSize.width; }
        (int)viewBox.width;

    /// <summary>
    /// Get the height of the page identified by the property PageNumber.
    /// </summary>
    public override int PixelHeight =>
        //get { return (int)this.templateSize.height; }
        (int)viewBox.height;

    /// <summary>
    /// Get the size of the page identified by the property PageNumber.
    /// </summary>
    public override XSize Size =>
        //get { return this.templateSize; }
        viewBox.Size;
    //XSize templateSize;

    /// <summary>
    /// Gets the view box of the form.
    /// </summary>
    public XRect ViewBox => viewBox;

    XRect viewBox;

    /// <summary>
    /// Gets 72, the horizontal resolution by design of a form object.
    /// </summary>
    public override double HorizontalResolution => 72;

    /// <summary>
    /// Gets 72 always, the vertical resolution by design of a form object.
    /// </summary>
    public override double VerticalResolution => 72;

    /// <summary>
    /// Gets or sets the bounding box.
    /// </summary>
    public XRect BoundingBox
    {
      get => boundingBox;
      set => boundingBox = value; // TODO: pdfForm = null
    }
    XRect boundingBox;

    /// <summary>
    /// Gets or sets the transformation matrix.
    /// </summary>
    public virtual XMatrix Transform
    {
      get => transform;
      set
      {
        if (formState == FormState.Finished)
          throw new InvalidOperationException("After a XPdfForm was once drawn it must not be modified.");
        transform = value;
      }
    }
    internal XMatrix transform = new XMatrix();  //XMatrix.Identity;

    internal PdfResources Resources
    {
      get
      {
        Debug.Assert(IsTemplate, "This function is for form templates only.");
        return PdfForm.Resources;
        //if (this.resources == null)
        //  this.resources = (PdfResources)this.pdfForm.Elements.GetValue(PdfFormXObject.Keys.Resources, VCF.Create); // VCF.CreateIndirect
        //return this.resources;
      }
    }
    //PdfResources resources;

    /// <summary>
    /// Implements the interface because the primary function is internal.
    /// </summary>
    PdfResources IContentStream.Resources => Resources;

    /// <summary>
    /// Gets the resource name of the specified font within this form.
    /// </summary>
    internal string GetFontName(XFont font, out PdfFont pdfFont)
    {
      Debug.Assert(IsTemplate, "This function is for form templates only.");
      pdfFont = document.FontTable.GetFont(font);
      Debug.Assert(pdfFont != null);
      string name = Resources.AddFont(pdfFont);
      return name;
    }

    string IContentStream.GetFontName(XFont font, out PdfFont pdfFont)
    {
      return GetFontName(font, out pdfFont);
    }

    /// <summary>
    /// Tries to get the resource name of the specified font data within this form.
    /// Returns null if no such font exists.
    /// </summary>
    internal string TryGetFontName(string idName, out PdfFont pdfFont)
    {
      Debug.Assert(IsTemplate, "This function is for form templates only.");
      pdfFont = document.FontTable.TryGetFont(idName);
      string name = null;
      if (pdfFont != null)
        name = Resources.AddFont(pdfFont);
      return name;
    }

    /// <summary>
    /// Gets the resource name of the specified font data within this form.
    /// </summary>
    internal string GetFontName(string idName, byte[] fontData, out PdfFont pdfFont)
    {
      Debug.Assert(IsTemplate, "This function is for form templates only.");
      pdfFont = document.FontTable.GetFont(idName, fontData);
      //pdfFont = new PdfType0Font(Owner, idName, fontData);
      //pdfFont.Document = this.document;
      Debug.Assert(pdfFont != null);
      string name = Resources.AddFont(pdfFont);
      return name;
    }

    string IContentStream.GetFontName(string idName, byte[] fontData, out PdfFont pdfFont)
    {
      return GetFontName(idName, fontData, out pdfFont);
    }

    /// <summary>
    /// Gets the resource name of the specified image within this form.
    /// </summary>
    internal string GetImageName(XImage image)
    {
      Debug.Assert(IsTemplate, "This function is for form templates only.");
      PdfImage pdfImage = document.ImageTable.GetImage(image);
      Debug.Assert(pdfImage != null);
      string name = Resources.AddImage(pdfImage);
      return name;
    }

    /// <summary>
    /// Implements the interface because the primary function is internal.
    /// </summary>
    string IContentStream.GetImageName(XImage image)
    {
      return GetImageName(image);
    }

    internal PdfFormXObject PdfForm
    {
      get
      {
        Debug.Assert(IsTemplate, "This function is for form templates only.");
        if (pdfForm.Reference == null)
          document.irefTable.Add(pdfForm);
        return pdfForm;
      }
    }

    /// <summary>
    /// Gets the resource name of the specified form within this form.
    /// </summary>
    internal string GetFormName(XForm form)
    {
      Debug.Assert(IsTemplate, "This function is for form templates only.");
      PdfFormXObject pdfForm = document.FormTable.GetForm(form);
      Debug.Assert(pdfForm != null);
      string name = Resources.AddForm(pdfForm);
      return name;
    }

    /// <summary>
    /// Implements the interface because the primary function is internal.
    /// </summary>
    string IContentStream.GetFormName(XForm form)
    {
      return GetFormName(form);
    }

    /// <summary>
    /// The PdfFormXObject gets invalid when PageNumber or transform changed. This is because a modification
    /// of an XPdfForm must not change objects that are already been drawn.
    /// </summary>
    internal PdfFormXObject pdfForm;

    internal XGraphicsPdfRenderer pdfRenderer;

#if WPF && !SILVERLIGHT
    /// <summary>
    /// Gets a value indicating whether this image is cmyk.
    /// </summary>
    /// <value><c>true</c> if this image is cmyk; otherwise, <c>false</c>.</value>
    public override bool IsCmyk => false; // not supported and not relevant

    /// <summary>
    /// Gets a value indicating whether this image is JPEG.
    /// </summary>
    /// <value><c>true</c> if this image is JPEG; otherwise, <c>false</c>.</value>
    public override bool IsJpeg => base.IsJpeg; // not supported and not relevant

    /// <summary>
    /// Gets the JPEG memory stream (if IsJpeg returns true).
    /// </summary>
    /// <value>The memory.</value>
    public override MemoryStream Memory => throw new NotImplementedException();
#endif
  }
}