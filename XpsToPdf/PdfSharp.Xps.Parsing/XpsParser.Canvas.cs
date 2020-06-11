using System;
using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a Canvas element.
    /// </summary>
    Canvas ParseCanvas()
    {
      Debug.Assert(reader.Name == "Canvas");
      Canvas canvas = new Canvas();
      try
      {
        bool isEmptyElement = reader.IsEmptyElement;
        while (MoveToNextAttribute())
        {
          switch (reader.Name)
          {
            case "Name":
              canvas.Name = reader.Value;
              break;

            case "RenderTransform":
              canvas.RenderTransform = ParseMatrixTransform(reader.Value);
              break;

            case "Clip":
              canvas.Clip = ParsePathGeometry(reader.Value);
              break;

            case "Opacity":
              canvas.Opacity = ParseDouble(reader.Value);
              break;

            case "OpacityMask":
              canvas.OpacityMask = ParseBrush(reader.Value);
              break;

            case "RenderOptions.EdgeMode":
              canvas.RenderOptions_EdgeMode = reader.Value;
              break;

            case "FixedPage.NavigateUri":
              canvas.FixedPage_NavigateUri = reader.Value;
              break;

            case "AutomationProperties.HelpText":
              canvas.AutomationProperties_HelpText = reader.Value;
              break;

            case "AutomationProperties.Name":
              canvas.AutomationProperties_Name = reader.Value;
              break;

            case "xml:lang":
              canvas.lang = reader.Value;
              break;

            case "xmlns:x":
              break;

            default:
              UnexpectedAttribute(reader.Name);
              break;
          }
        }
        if (!isEmptyElement)
        {
          MoveToNextElement();
          while (reader.IsStartElement())
          {
            switch (reader.Name)
            {
              case "Canvas.Resources":
                MoveToNextElement();
                ResourceDictionary rd = new ResourceDictionary();
                canvas.Resources = rd;
                rd.Parent = canvas;
                rd.ResourceParent = ResourceDictionaryStack.Current;
                ResourceDictionaryStack.Push(rd);
                ParseResourceDictionary(rd);
                MoveToNextElement();
                break;

              case "Canvas.RenderTransform":
                MoveToNextElement();
                canvas.RenderTransform = ParseMatrixTransform();
                MoveToNextElement();
                break;

              case "Canvas.Clip":
                MoveToNextElement();
                canvas.Clip = ParsePathGeometry();
                break;

              case "Canvas.OpacityMask":
                MoveToNextElement();
                canvas.OpacityMask = ParseBrush();
                canvas.OpacityMask.Parent = canvas;
                break;

              case "Path":
                {
                  XpsModel.Path path = ParsePath();
#if DEBUG
                  if (!String.IsNullOrEmpty(path.Name))
                    Debug.WriteLine("Path: " + path.Name);
#endif
                  canvas.Content.Add(path);
                  path.Parent = canvas;
                }
                break;

              case "Glyphs":
                {
                  Glyphs glyphs = ParseGlyphs();
                  canvas.Content.Add(glyphs);
                  glyphs.Parent = canvas;
                }
                break;

              case "Canvas":
                {
                  Canvas canvas2 = ParseCanvas();
                  canvas.Content.Add(canvas2);
                  canvas2.Parent = canvas;
                }
                break;

              case "mc:AlternateContent":
              case "mc:Choice":
                MoveToNextElement();
                //canvas.Content.Add(ParseCanvas());
                break;

              default:
                Debugger.Break();
                break;
            }
          }
        }
        MoveToNextElement();
      }
      finally
      {
        // If the current ResourceDictionary is from this Canvas, pop it.
        if (canvas != null && canvas.Resources != null)
        {
          if (ReferenceEquals(canvas.Resources, ResourceDictionaryStack.Current))
            ResourceDictionaryStack.Pop();
        }
      }
      return canvas;
    }
  }
}