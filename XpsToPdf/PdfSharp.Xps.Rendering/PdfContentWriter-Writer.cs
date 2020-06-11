using System.Diagnostics;
using System.Globalization;
using System.Text;
using PdfSharp.Xps.XpsModel;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Internal;
using PdfSharp.Drawing;

namespace PdfSharp.Xps.Rendering
{
  partial class PdfContentWriter
  {
    /// <summary>
    /// Writes a string literally to the content stream.
    /// </summary>
    public void WriteLiteral(string value)
    {
      WriteIndent();
      content.Append(value);
    }

    /// <summary>
    /// Writes a formatted string literally to the content stream.
    /// </summary>
    public void WriteLiteral(string format, params object[] args)
    {
      WriteIndent();
      content.AppendFormat(CultureInfo.InvariantCulture, format, args);
    }

    //internal void AppendRgb(float r, float g, float b, string op)
    //{
    //  this.content.AppendFormat(CultureInfo.InvariantCulture, "{0:0.###} {1:0.###} {2:0.###} {3}", r, g, b, op);
    //}

    public void WriteRgb(Color color, string op)
    {
      WriteIndent();
      content.AppendFormat(CultureInfo.InvariantCulture, "{0:0.###} {1:0.###} {2:0.###} {3}", color.R / 255.0, color.G / 255.0, color.B / 255.0, op);
    }

    public void WriteMatrix(XMatrix matrix)
    {
      WriteIndent();
      content.Append(PdfEncoders.ToString(matrix) + " cm\n");
    }

    public void WriteGraphicsState(PdfExtGState extGState)
    {
      string name = Resources.AddExtGState(extGState);
      WriteLiteral(name + " gs\n");
    }

    public void WriteForm(XForm form)
    {
      string name = Resources.AddForm(form.PdfForm);
      WriteLiteral(name + " Do\n");
    }

    /// <summary>
    /// Helps to make the content stream more human readable.
    /// </summary>
    [Conditional("DEBUG")]
    void WriteIndent()
    {
      content.Append(new string(' ', 2 * graphicsState.Level));
    }

    StringBuilder content;
    PdfTraceLevel traceLevel = PdfTraceLevel.Verbose;
  }
}