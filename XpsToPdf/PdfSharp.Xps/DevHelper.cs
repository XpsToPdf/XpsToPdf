using System.Diagnostics;

namespace PdfSharp.Xps
{
  /// <summary>
  /// Helper functions for developing the XPS to PDF converter.
  /// </summary>
  static class DevHelper
  {
    public static void NotImplemented(string message)
    {
      Debug.WriteLine("XPS feature not implemented: " + message);
    }

    /// <summary>
    /// Gets a value indicating whether to render comments into the PDF dictionaries
    /// for easier debugging.
    /// </summary>
    public static bool RenderComments
    {
#if DEBUG_
      get { return true; }
#else
      get { return false; }
#endif
    }

    /// <summary>
    /// Gets a value indicating whether to draw a black border around the bounding box
    /// of a pattern to make it easier to find transformation issues.
    /// </summary>
    public static bool BorderPatterns
    {
#if DEBUG_
      get { return true; }
#else
      get { return false; }
#endif
    }

    /// <summary>
    /// Gets a value indicating whether to flatten ArcSegments instead of calculation Beziér curves.
    /// </summary>
    public static bool FlattenArcSegments
    {
#if DEBUG
      get { return flattenArcSegments; }
#else
      get { return false; }
#endif
      set { flattenArcSegments = value; }
    }
    static bool flattenArcSegments = false;

    /// <summary>
    /// Gets a value indicating whether to flatten PolyQuadraticBezierSegment instead of calculation Beziér curves.
    /// </summary>
    public static bool FlattenPolyQuadraticBezierSegment
    {
#if DEBUG
      get { return flattenPolyQuadraticBezierSegment; }
#else
      get { return true; }
#endif
      set { flattenPolyQuadraticBezierSegment = value; }
    }
    static bool flattenPolyQuadraticBezierSegment = false;
  }
}