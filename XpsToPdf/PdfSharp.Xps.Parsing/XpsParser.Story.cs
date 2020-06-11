using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a Story element.
    /// </summary>
    Story ParseStory()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      Story story = new Story();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "StoryName":
            story.StoryName = reader.Value;
            break;

          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return story;
    }
  }
}