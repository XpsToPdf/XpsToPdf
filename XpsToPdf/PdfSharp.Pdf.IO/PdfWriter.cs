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
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.IO;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Security;
using PdfSharp.Pdf.Internal;

namespace PdfSharp.Pdf.IO
{
  /// <summary>
  /// Represents a writer for generation of PDF streams. 
  /// </summary>
  internal class PdfWriter
  {
    public PdfWriter(Stream pdfStream, PdfStandardSecurityHandler securityHandler)
    {
      stream = pdfStream;
      this.securityHandler = securityHandler;
      //System.Xml.XmlTextWriter
#if DEBUG
      layout = PdfWriterLayout.Verbose;
#endif
    }

    public void Close(bool closeUnderlyingStream)
    {
      if (stream != null && closeUnderlyingStream)
      {
        stream.Close();
        stream = null;
      }
    }

    public void Close()
    {
      Close(true);
    }

    public int Position => (int)stream.Position;

    public PdfWriterLayout Layout
    {
      get => layout;
      set => layout = value;
    }
    PdfWriterLayout layout;

    public PdfWriterOptions Options
    {
      get => options;
      set => options = value;
    }
    PdfWriterOptions options;

    // -----------------------------------------------------------

    /// <summary>
    /// Writes the specified value to the PDF stream.
    /// </summary>
    public void Write(bool value)
    {
      WriteSeparator(CharCat.Character);
      WriteRaw(value ? bool.TrueString : bool.FalseString);
      lastCat = CharCat.Character;
    }

    /// <summary>
    /// Writes the specified value to the PDF stream.
    /// </summary>
    public void Write(PdfBoolean value)
    {
      WriteSeparator(CharCat.Character);
      WriteRaw(value.Value ? "true" : "false");
      lastCat = CharCat.Character;
    }

    /// <summary>
    /// Writes the specified value to the PDF stream.
    /// </summary>
    public void Write(int value)
    {
      WriteSeparator(CharCat.Character);
      WriteRaw(value.ToString(CultureInfo.InvariantCulture));
      lastCat = CharCat.Character;
    }

    /// <summary>
    /// Writes the specified value to the PDF stream.
    /// </summary>
    public void Write(uint value)
    {
      WriteSeparator(CharCat.Character);
      WriteRaw(value.ToString(CultureInfo.InvariantCulture));
      lastCat = CharCat.Character;
    }

    /// <summary>
    /// Writes the specified value to the PDF stream.
    /// </summary>
    public void Write(PdfInteger value)
    {
      WriteSeparator(CharCat.Character);
      lastCat = CharCat.Character;
      WriteRaw(value.Value.ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Writes the specified value to the PDF stream.
    /// </summary>
    public void Write(PdfUInteger value)
    {
      WriteSeparator(CharCat.Character);
      lastCat = CharCat.Character;
      WriteRaw(value.Value.ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Writes the specified value to the PDF stream.
    /// </summary>
    public void Write(double value)
    {
      WriteSeparator(CharCat.Character);
      WriteRaw(value.ToString("0.###", CultureInfo.InvariantCulture));
      lastCat = CharCat.Character;
    }

    /// <summary>
    /// Writes the specified value to the PDF stream.
    /// </summary>
    public void Write(PdfReal value)
    {
      WriteSeparator(CharCat.Character);
      WriteRaw(value.Value.ToString("0.###", CultureInfo.InvariantCulture));
      lastCat = CharCat.Character;
    }

    /// <summary>
    /// Writes the specified value to the PDF stream.
    /// </summary>
    public void Write(PdfString value)
    {
      WriteSeparator(CharCat.Delimiter);
#if true
      PdfStringEncoding encoding = (PdfStringEncoding)(value.Flags & PdfStringFlags.EncodingMask);
      string pdf;
      if ((value.Flags & PdfStringFlags.HexLiteral) == 0)
        pdf = PdfEncoders.ToStringLiteral(value.Value, encoding, SecurityHandler);
      else
        pdf = PdfEncoders.ToHexStringLiteral(value.Value, encoding, SecurityHandler);
      WriteRaw(pdf);
#else
      switch (value.Flags & PdfStringFlags.EncodingMask)
      {
        case PdfStringFlags.Undefined:
        case PdfStringFlags.PDFDocEncoding:
          if ((value.Flags & PdfStringFlags.HexLiteral) == 0)
            WriteRaw(PdfEncoders.DocEncode(value.Value, false));
          else
            WriteRaw(PdfEncoders.DocEncodeHex(value.Value, false));
          break;

        case PdfStringFlags.WinAnsiEncoding:
          throw new NotImplementedException("Unexpected encoding: WinAnsiEncoding");

        case PdfStringFlags.Unicode:
          if ((value.Flags & PdfStringFlags.HexLiteral) == 0)
            WriteRaw(PdfEncoders.DocEncode(value.Value, true));
          else
            WriteRaw(PdfEncoders.DocEncodeHex(value.Value, true));
          break;

        case PdfStringFlags.StandardEncoding:
        case PdfStringFlags.MacRomanEncoding:
        case PdfStringFlags.MacExpertEncoding:
        default:
          throw new NotImplementedException("Unexpected encoding");
      }
#endif
      lastCat = CharCat.Delimiter;
    }

    /// <summary>
    /// Writes the specified value to the PDF stream.
    /// </summary>
    public void Write(PdfName value)
    {
      WriteSeparator(CharCat.Delimiter, '/');
      string name = value.Value;

      StringBuilder pdf = new StringBuilder("/");
      for (int idx = 1; idx < name.Length; idx++)
      {
        char ch = name[idx];
        Debug.Assert(ch < 256);
        if (ch > ' ')
          switch (ch)
          {
            // TODO: is this all?
            case '%':
            case '/':
            case '<':
            case '>':
            case '(':
            case ')':
            case '#':
              break;

            default:
              pdf.Append(name[idx]);
              continue;
          }
        pdf.AppendFormat("#{0:X2}", (int)name[idx]);
      }
      WriteRaw(pdf.ToString());
      lastCat = CharCat.Character;
    }

    public void Write(PdfLiteral value)
    {
      WriteSeparator(CharCat.Character);
      WriteRaw(value.Value);
      lastCat = CharCat.Character;
    }

    public void Write(PdfRectangle rect)
    {
      WriteSeparator(CharCat.Delimiter, '/');
      WriteRaw(PdfEncoders.Format("[{0:0.###} {1:0.###} {2:0.###} {3:0.###}]", rect.X1, rect.Y1, rect.X2, rect.Y2));
      lastCat = CharCat.Delimiter;
    }

    public void Write(PdfReference iref)
    {
      WriteSeparator(CharCat.Character);
      WriteRaw(iref.ToString());
      lastCat = CharCat.Character;
    }

    public void WriteDocString(string text, bool unicode)
    {
      WriteSeparator(CharCat.Delimiter);
      //WriteRaw(PdfEncoders.DocEncode(text, unicode));
      byte[] bytes;
      if (!unicode)
        bytes = PdfEncoders.DocEncoding.GetBytes(text);
      else
        bytes = PdfEncoders.UnicodeEncoding.GetBytes(text);
      bytes = PdfEncoders.FormatStringLiteral(bytes, unicode, true, false, securityHandler);
      Write(bytes);
      lastCat = CharCat.Delimiter;
    }

    public void WriteDocString(string text)
    {
      WriteSeparator(CharCat.Delimiter);
      //WriteRaw(PdfEncoders.DocEncode(text, false));
      byte[] bytes = PdfEncoders.DocEncoding.GetBytes(text);
      bytes = PdfEncoders.FormatStringLiteral(bytes, false, false, false, securityHandler);
      Write(bytes);
      lastCat = CharCat.Delimiter;
    }

    public void WriteDocStringHex(string text)
    {
      WriteSeparator(CharCat.Delimiter);
      //WriteRaw(PdfEncoders.DocEncodeHex(text));
      byte[] bytes = PdfEncoders.DocEncoding.GetBytes(text);
      bytes = PdfEncoders.FormatStringLiteral(bytes, false, false, true, securityHandler);
      stream.Write(bytes, 0, bytes.Length);
      lastCat = CharCat.Delimiter;
    }

    /// <summary>
    /// Begins a direct or indirect dictionary or array.
    /// </summary>
    public void WriteBeginObject(PdfObject value)
    {
      bool indirect = value.IsIndirect;
      if (indirect)
      {
        WriteObjectAddress(value);
        if (securityHandler != null)
          securityHandler.SetHashKey(value.ObjectID);
      }
      stack.Add(new StackItem(value));
      if (indirect)
      {
        if (value is PdfArray)
          WriteRaw("[\n");
        else if (value is PdfDictionary)
          WriteRaw("<<\n");
        lastCat = CharCat.NewLine;
      }
      else
      {
        if (value is PdfArray)
        {
          WriteSeparator(CharCat.Delimiter);
          WriteRaw('[');
          lastCat = CharCat.Delimiter;
        }
        else if (value is PdfDictionary)
        {
          NewLine();
          WriteSeparator(CharCat.Delimiter);
          WriteRaw("<<\n");
          lastCat = CharCat.NewLine;
        }
      }
      if (layout == PdfWriterLayout.Verbose)
        IncreaseIndent();
    }

    /// <summary>
    /// Ends a direct or indirect dictionary or array.
    /// </summary>
    public void WriteEndObject()
    {
      int count = stack.Count;
      Debug.Assert(count > 0, "PdfWriter stack underflow.");

      StackItem stackItem = (StackItem)stack[count - 1];
      stack.RemoveAt(count - 1);

      PdfObject value = stackItem.Object;
      bool indirect = value.IsIndirect;
      if (layout == PdfWriterLayout.Verbose)
        DecreaseIndent();
      if (value is PdfArray)
      {
        if (indirect)
        {
          //WriteRaw("\n");
          //WriteIndent();
          WriteRaw("\n]\n");
          lastCat = CharCat.NewLine;
        }
        else
        {
          WriteRaw("]");
          lastCat = CharCat.Delimiter;
        }
      }
      else if (value is PdfDictionary)
      {
        if (indirect)
        {
          if (!stackItem.HasStream)
            if (lastCat == CharCat.NewLine)
              WriteRaw(">>\n");
            else
              WriteRaw(" >>\n");
        }
        else
        {
          Debug.Assert(!stackItem.HasStream, "Direct object with stream??");
          WriteSeparator(CharCat.NewLine);
          WriteRaw(">>\n");
          lastCat = CharCat.NewLine;
        }
      }
      if (indirect)
      {
        NewLine();
        WriteRaw("endobj\n");
        if (layout == PdfWriterLayout.Verbose)
          WriteRaw("%--------------------------------------------------------------------------------------------------\n");
      }
    }

    /// <summary>
    /// Writes the stream of the specified dictionary.
    /// </summary>
    public void WriteStream(PdfDictionary value, bool omitStream)
    {
      StackItem stackItem = (StackItem)stack[stack.Count - 1];
      Debug.Assert(stackItem.Object is PdfDictionary);
      Debug.Assert(stackItem.Object.IsIndirect);
      stackItem.HasStream = true;

      if (lastCat == CharCat.NewLine)
        WriteRaw(">>\nstream\n");
      else
        WriteRaw(" >>\nstream\n");

      if (omitStream)
        WriteRaw("  «...stream content omitted...»\n");  // useful for debugging only
      else
      {
        byte[] bytes = value.Stream.Value;
        if (bytes.Length != 0)
        {
          if (securityHandler != null)
          {
            bytes = (byte[])bytes.Clone();
            bytes = securityHandler.EncryptBytes(bytes);
          }
          Write(bytes);
          if (lastCat != CharCat.NewLine)
            WriteRaw('\n');
        }
      }
      WriteRaw("endstream\n");
    }

    public void WriteRaw(string rawString)
    {
      if (rawString == null || rawString.Length == 0)
        return;
      //AppendBlank(rawString[0]);
      byte[] bytes = PdfEncoders.RawEncoding.GetBytes(rawString);
      stream.Write(bytes, 0, bytes.Length);
      lastCat = GetCategory((char)bytes[bytes.Length - 1]);
    }

    public void WriteRaw(char ch)
    {
      Debug.Assert((int)ch < 256, "Raw character greater than 255 dedected.");
      //AppendBlank(ch);
      stream.WriteByte((byte)ch);
      lastCat = GetCategory(ch);
    }

    public void Write(byte[] bytes)
    {
      if (bytes == null || bytes.Length == 0)
        return;
      stream.Write(bytes, 0, bytes.Length);
      lastCat = GetCategory((char)bytes[bytes.Length - 1]);
    }

    void WriteObjectAddress(PdfObject value)
    {
      if (layout == PdfWriterLayout.Verbose)
        WriteRaw(String.Format("{0} {1} obj   % {2}\n",
          value.ObjectID.ObjectNumber, value.ObjectID.GenerationNumber,
          value.GetType().FullName));
      else
        WriteRaw(String.Format("{0} {1} obj\n", value.ObjectID.ObjectNumber, value.ObjectID.GenerationNumber));
    }

    public void WriteFileHeader(PdfDocument document)
    {
      StringBuilder header = new StringBuilder("%PDF-");
      int version = document.version;
      header.Append((version / 10).ToString(CultureInfo.InvariantCulture) + "." + (version % 10).ToString(CultureInfo.InvariantCulture) + "\n%\xD3\xF4\xCC\xE1\n");
      WriteRaw(header.ToString());

      if (layout == PdfWriterLayout.Verbose)
      {
        WriteRaw(String.Format("% PDFsharp Version {0} (verbose mode)\n", VersionInfo.Version));
        // Keep some space for later fix-up.
        commentPosition = (int)stream.Position + 2;
        WriteRaw("%                                                \n");
        WriteRaw("%                                                \n");
        WriteRaw("%                                                \n");
        WriteRaw("%                                                \n");
        WriteRaw("%                                                \n");
        WriteRaw("%--------------------------------------------------------------------------------------------------\n");
      }
    }

    public void WriteEof(PdfDocument document, int startxref)
    {
      WriteRaw("startxref\n");
      WriteRaw(startxref.ToString(CultureInfo.InvariantCulture));
      WriteRaw("\n%%EOF\n");
      int fileSize = (int)stream.Position;
      if (layout == PdfWriterLayout.Verbose)
      {
        TimeSpan duration = DateTime.Now - document.creation;

        stream.Position = commentPosition;
        WriteRaw("Creation date: " + document.creation.ToString("G"));
        stream.Position = commentPosition + 50;
        WriteRaw("Creation time: " + duration.TotalSeconds.ToString("0.000", CultureInfo.InvariantCulture) + " seconds");
        stream.Position = commentPosition + 100;
        WriteRaw("File size: " + fileSize.ToString(CultureInfo.InvariantCulture) + " bytes");
        stream.Position = commentPosition + 150;
        WriteRaw("Pages: " + document.Pages.Count.ToString(CultureInfo.InvariantCulture));
        stream.Position = commentPosition + 200;
        WriteRaw("Objects: " + document.irefTable.objectTable.Count.ToString(CultureInfo.InvariantCulture));
      }
    }

    /// <summary>
    /// Gets or sets the indentation for a new indentation level.
    /// </summary>
    internal int Indent
    {
      get => indent;
      set => indent = value;
    }
    protected int indent = 2;
    protected int writeIndent = 0;

    /// <summary>
    /// Increases indent level.
    /// </summary>
    void IncreaseIndent()
    {
      writeIndent += indent;
    }

    /// <summary>
    /// Decreases indent level.
    /// </summary>
    void DecreaseIndent()
    {
      writeIndent -= indent;
    }

    ///// <summary>
    ///// Returns an indent string of blanks.
    ///// </summary>
    //static string Ind(int _indent)
    //{
    //  return new String(' ', _indent);
    //}

    /// <summary>
    /// Gets an indent string of current indent.
    /// </summary>
    string IndentBlanks => new string(' ', writeIndent);

    void WriteIndent()
    {
      WriteRaw(IndentBlanks);
    }

    void WriteSeparator(CharCat cat, char ch)
    {
      switch (lastCat)
      {
        case CharCat.NewLine:
          if (layout == PdfWriterLayout.Verbose)
            WriteIndent();
          break;

        case CharCat.Delimiter:
          break;

        case CharCat.Character:
          if (layout == PdfWriterLayout.Verbose)
          {
            //if (cat == CharCat.Character || ch == '/')
            stream.WriteByte((byte)' ');
          }
          else
          {
            if (cat == CharCat.Character)
              stream.WriteByte((byte)' ');
          }
          break;
      }
    }

    void WriteSeparator(CharCat cat)
    {
      WriteSeparator(cat, '\0');
    }

    public void NewLine()
    {
      if (lastCat != CharCat.NewLine)
        WriteRaw('\n');
    }

    CharCat GetCategory(char ch)
    {
      if (Lexer.IsDelimiter(ch))
        return CharCat.Delimiter;
      if (ch == Chars.LF)
        return CharCat.NewLine;
      return CharCat.Character;
    }

    enum CharCat
    {
      NewLine,
      Character,
      Delimiter,
    };
    CharCat lastCat;

    /// <summary>
    /// Gets the underlying stream.
    /// </summary>
    internal Stream Stream => stream;

    Stream stream;

    internal PdfStandardSecurityHandler SecurityHandler
    {
      get => securityHandler;
      set => securityHandler = value;
    }
    PdfStandardSecurityHandler securityHandler;

    class StackItem
    {
      public StackItem(PdfObject value)
      {
        Object = value;
      }

      public PdfObject Object;
      public bool HasStream;
    }

    List<StackItem> stack = new List<StackItem>();
    int commentPosition;
  }
}
