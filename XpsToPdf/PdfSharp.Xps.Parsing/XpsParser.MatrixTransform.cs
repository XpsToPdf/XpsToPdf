using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a MatrixTransform element.
    /// </summary>
    MatrixTransform ParseMatrixTransform()
    {
      AssertElement("MatrixTransform");
      MatrixTransform transform = new MatrixTransform();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "Matrix":
            transform.Matrix = Matrix.Parse(reader.Value);
            break;

          case "x:Key":
            transform.Key = reader.Value;
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveBeyondThisElement();
      return transform;
    }

    /// <summary>
    /// Parses a MatrixTransform attribute.
    /// </summary>
    MatrixTransform ParseMatrixTransform(string value)
    {
      MatrixTransform transform = TryParseStaticResource<MatrixTransform>(value);
      if (transform != null)
        return transform;

      return new MatrixTransform(Matrix.Parse(value));
    }
  }
}