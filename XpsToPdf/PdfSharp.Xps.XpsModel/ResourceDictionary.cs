using System.Collections.Generic;

namespace PdfSharp.Xps.XpsModel
{
  /// <summary>
  /// Defines a set of reusable resource definitions that can be used as property values in the fixed page markup.
  /// </summary>
  class ResourceDictionary : XpsElement
  {
    /// <summary>
    /// Specifies the URI of a part containing markup for a resource dictionary.
    /// The URI MUST refer to a part in the package [M2.1]. 
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Gets or sets the parent ResourceDictionary of this element.
    /// </summary>
    public ResourceDictionary ResourceParent
    {
      get => parentResourceDictionary;
      set => parentResourceDictionary = value;
    }
    ResourceDictionary parentResourceDictionary;

    /// <summary>
    /// Gets the XpsElement with the specified key, or null, if no such value exists.
    /// </summary>
    public XpsElement this[string key]
    {
      get 
      {
        XpsElement element;
        elements.TryGetValue(key, out element);
        return element; 
      }
    }
    internal Dictionary<string, XpsElement> elements = new Dictionary<string, XpsElement>();
  }
}