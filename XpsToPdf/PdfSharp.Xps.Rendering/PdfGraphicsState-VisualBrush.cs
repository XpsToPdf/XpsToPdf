using System;
using PdfSharp.Drawing;
using PdfSharp.Xps.XpsModel;

#pragma warning disable 414, 169 // incomplete code state

namespace PdfSharp.Xps.Rendering
{
  partial class PdfGraphicsState
  {
    [Obsolete]
    void RealizeVisualBrush(VisualBrush brush, ref XForm xform)
    {
      //Debug.Assert(xform != null);

      xform = new XForm(writer.Owner, new XRect(brush.Viewbox.X * 3 / 4, brush.Viewbox.Y * 3 / 4, brush.Viewbox.Width * 3 / 4, brush.Viewbox.Height * 3 / 4));

      Visual visual = brush.Visual;
      PdfContentWriter formWriter = new PdfContentWriter(writer.Context, xform, RenderMode.Default);


      //formWriter.Size = brush.Viewport.Size;
      formWriter.BeginContent(false);
      formWriter.WriteElement(visual);
      formWriter.EndContent();
    }
  }
}