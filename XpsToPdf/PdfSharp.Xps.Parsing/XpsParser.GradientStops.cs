using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.IO;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a sequence of GradientStop element.
    /// </summary>
    GradientStopCollection ParseGradientStops()
    {
      GradientStopCollection gradientStops = new GradientStopCollection();
      if (!reader.IsEmptyElement)
      {
        MoveToNextElement();
        while (reader.IsStartElement())
        {
          GradientStop gs = ParseGradientStop();
          gradientStops.Add(gs);
        }
        MoveToNextElement();
      }
      return gradientStops;
    }
  }
}