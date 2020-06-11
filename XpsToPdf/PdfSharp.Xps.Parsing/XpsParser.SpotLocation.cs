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
    /// Parses a SpotLocation element.
    /// </summary>
    SpotLocation ParseSpotLocation()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      SpotLocation spotLocation = new SpotLocation();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "StartX":
            spotLocation.StartX = double.Parse(reader.Value);
            break;

          case "StartY":
            spotLocation.StartY = double.Parse(reader.Value);
            break;

          case "PageURI":
            spotLocation.PageURI = reader.Value;
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return spotLocation;
    }
  }
}