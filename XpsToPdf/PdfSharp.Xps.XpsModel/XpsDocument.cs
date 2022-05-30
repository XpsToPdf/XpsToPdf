using System;
using System.IO;
using System.IO.Packaging;
using System.Xml;

namespace PdfSharp.Xps.XpsModel
{
  /// <summary>
  /// Represents an XpsDocument that can be converted to PDF by PDFsharp.
  /// </summary>
  public sealed class XpsDocument : IDisposable
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="XpsDocument"/> class from a stream.
    /// </summary>
    XpsDocument(Stream stream)
    {
      package = System.IO.Packaging.Package.Open(stream) as ZipPackage;
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XpsDocument"/> class from a path.
    /// </summary>
    XpsDocument(string path)
    {
      package = System.IO.Packaging.Package.Open(path) as ZipPackage;
      this.path = path;
      Initialize();
    }

    /// <summary>
    /// Opens an XPS document from the specifed stream.
    /// </summary>
    public static XpsDocument Open(Stream stream)
    {
      return new XpsDocument(stream);
    }

    /// <summary>
    /// Opens an XPS document from the specifed path.
    /// </summary>
    public static XpsDocument Open(string path)
    {
      return new XpsDocument(path);
    }

    /// <summary>
    /// Initializes the primary fixed payload.
    /// </summary>
    void Initialize()
    {
      parts = package.GetParts();
      // Assume only one fixed payload 
      fpayload = new FixedPayload(this);
    }

    void IDisposable.Dispose()
    {
      if (!disposed)
      {
        try
        {
          parts = null;
          fpayload = null;
          if (package != null)
          {
            package.Close();
            package = null;
          }
        }
        finally
        {
          disposed = true;
        }
        GC.SuppressFinalize(this);
      }
    }
    bool disposed;

    /// <summary>
    /// Closes this instance and the underlying ZIP package.
    /// </summary>
    public void Close()
    {
      ((IDisposable)this).Dispose();
    }

    internal string Path => path;
    string path;

    /// <summary>
    /// Gets the number of fixed documents.
    /// </summary>
    public int DocumentCount => fpayload.DocumentCount;

    /// <summary>
    /// Gets a read-only collection of the fixed documents of this XPS document.
    /// </summary>
    public FixedDocumentCollection Documents
    {
      get
      {
        if (documents == null)
          documents = new FixedDocumentCollection(fpayload);
        return documents;
      }
    }
    FixedDocumentCollection documents;

    internal XmlTextReader GetPartAsXmlReader(string uri)
    {
      return GetPartAsXmlReader(package, uri);
    }

    internal static XmlTextReader GetPartAsXmlReader(ZipPackage package, string uriString)
    {
      // HACK: just make it work...
      if (!uriString.StartsWith("/"))
        uriString = "/" + uriString;

      // Documents with relative uri exists.
      if (uriString.StartsWith("/.."))
        uriString = uriString.Substring(3);

      ZipPackagePart part = package.GetPart(new Uri(uriString, UriKind.Relative)) as ZipPackagePart;
      string xml = String.Empty;
      using (Stream stream = part.GetStream())
      {
        using (StreamReader sr = new StreamReader(stream))
        {
          xml = sr.ReadToEnd();
        }
      }
      XmlTextReader reader = new XmlTextReader(new StringReader(xml));
      return reader;
    }

    internal byte[] GetPartAsBytes(string uriString)
    {
      return GetPartAsBytes(package, uriString);
    }

    internal static byte[] GetPartAsBytes(ZipPackage package, string uriString)
    {
      Uri target = new Uri(uriString, UriKind.Relative);
#if true
      if (!uriString.StartsWith("/"))
      {
        //target = PackUriHelper.ResolvePartUri(package.t.GetRelationship Uri("/documents2/3/Pages", UriKind.RelativeOrAbsolute), target);
        //PackagePartCollection coll = package.GetParts();
        // HACK: introduce a "CurrentPart"
        target = PackUriHelper.ResolvePartUri(new Uri("/documents/1/Pages/1.page", UriKind.RelativeOrAbsolute), target);
      }
#else
      // HACK: just make it go...
      if (!uriString.StartsWith("/"))
        uriString = "/" + uriString;

      // Documents with relative uri exists.
      if (uriString.StartsWith("/.."))
        uriString = uriString.Substring(3);
#endif
      var part = package.GetPart(target);

      using (var srcStream = part.GetStream())
      {
          var buffer = new byte[srcStream.Length];
          using (var dstStream = new MemoryStream(buffer))
          {
              srcStream.CopyTo(dstStream);

              return buffer;
          }
      }
    }

    /// <summary>
    /// Gets the underlying ZIP package.
    /// </summary>
    internal ZipPackage Package => package;

    ZipPackage package;

    /// <summary>
    /// Gets the underlying ZIP package parts collection.
    /// </summary>
    internal PackagePartCollection Parts => parts;

    PackagePartCollection parts;

    FixedPayload fpayload;
  }
}