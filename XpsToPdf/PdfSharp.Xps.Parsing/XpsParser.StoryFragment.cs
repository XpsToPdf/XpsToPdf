using System.Diagnostics;
using PdfSharp.Xps.XpsModel;

namespace PdfSharp.Xps.Parsing
{
  partial class XpsParser
  {
    /// <summary>
    /// Parses a StoryFragment element.
    /// </summary>
    StoryFragment ParseStoryFragment()
    {
      Debug.Assert(reader.Name == "");
      bool isEmptyElement = reader.IsEmptyElement;
      StoryFragment storyFragment = new StoryFragment();
      while (MoveToNextAttribute())
      {
        switch (reader.Name)
        {
          case "StoryName":
            storyFragment.StoryName = reader.Value;
            break;

          case "FragmentName":
            storyFragment.FragmentName = reader.Value;
            break;

          case "FragmentType":
            storyFragment.FragmentType = reader.Value;
            break;
          
          default:
            UnexpectedAttribute(reader.Name);
            break;
        }
      }
      MoveToNextElement();
      return storyFragment;
    }
  }
}