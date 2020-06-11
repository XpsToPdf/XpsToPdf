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
    /// Parses a NamedElement element.
    /// </summary>
    NamedElement ParseNamedElement()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      NamedElement namedElement = new NamedElement();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "NameReference":
            namedElement.NameReference = reader.Value;
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return namedElement;
    }
  }
}