using System;
using System.Globalization;
using PdfSharp.Internal;

#pragma warning disable 414, 169, 649 // incomplete code state

namespace PdfSharp.Xps.XpsModel
{
  struct Color
  {
    public byte A
    {
      get => sRgbColor.a;
      set
      {
        sRgbColor.a = value;
        scRgbColor.a = ((float)value) / 255f;
      }
    }

    public byte R
    {
      get => sRgbColor.r;
      set
      {
        sRgbColor.r = value;
        scRgbColor.r = ColorHelper.sRgbToScRgb(value);
      }
    }

    public byte G
    {
      get => sRgbColor.g;
      set
      {
        sRgbColor.g = value;
        scRgbColor.g = ColorHelper.sRgbToScRgb(value);
      }
    }

    public byte B
    {
      get => sRgbColor.b;
      set
      {
        sRgbColor.b = value;
        scRgbColor.b = ColorHelper.sRgbToScRgb(value);
      }
    }

    public float ScA
    {
      get => scRgbColor.a;
      set
      {
        scRgbColor.a = value;
        if (value < 0f)
          sRgbColor.a = 0;
        else if (value > 1f)
          sRgbColor.a = 0xff;
        else
          sRgbColor.a = (byte)(value * 255f);
      }
    }

    public float ScR
    {
      get => scRgbColor.r;
      set
      {
        scRgbColor.r = value;
        sRgbColor.r = ColorHelper.ScRgbTosRgb(value);
      }
    }

    public float ScG
    {
      get => scRgbColor.g;
      set
      {
        scRgbColor.g = value;
        sRgbColor.g = ColorHelper.ScRgbTosRgb(value);
      }
    }

    public float ScB
    {
      get => scRgbColor.b;
      set
      {
        scRgbColor.b = value;
        sRgbColor.b = ColorHelper.ScRgbTosRgb(value);
      }
    }

    public static implicit operator Drawing.XColor(Color clr)
    {
      return Drawing.XColor.FromArgb(clr.A, clr.R, clr.G, clr.B);
    }

    internal static Color FromArgb(byte a, byte r, byte g, byte b)
    {
      Color clr = new Color();
      clr.A = a;
      clr.R = r;
      clr.G = g;
      clr.B = b;
      return clr;
    }

    internal static Color Parse(string value)
    {
      Color clr = new Color();

      int length = value.Length;
      if (value.StartsWith("#"))
      {
        if (length == 7)
        {
          clr.colorType = ColorType.scRGB;
          uint val = UInt32.Parse(value.Substring(1), NumberStyles.HexNumber);
          clr.A = 0xFF;
          clr.R = (byte)((val >> 16) & 0xFF);
          clr.G = (byte)((val >> 8) & 0xFF);
          clr.B = (byte)(val & 0xFF);
        }
        else if (length == 9)
        {
          clr.colorType = ColorType.scRGBwithAlpha;
          uint val = UInt32.Parse(value.Substring(1), NumberStyles.HexNumber);
          clr.A = (byte)((val >> 24) & 0xFF);
          clr.R = (byte)((val >> 16) & 0xFF);
          clr.G = (byte)((val >> 8) & 0xFF);
          clr.B = (byte)(val & 0xFF);
        }
      }
      else
      {
        // TODO
        if (value.StartsWith("{StaticResource"))
        {
          DevHelper.NotImplemented("Color StaticResource: " + value);
          // HACK: just continue
          return FromArgb(255, 0, 128, 0);
        }
        else if (value.StartsWith("sc#"))
        {
          string[] xx = value.Substring(3).Split(',');
          if (xx.Length == 3)
          {
            clr.ScR = float.Parse(xx[0], CultureInfo.InvariantCulture);
            clr.ScG = float.Parse(xx[1], CultureInfo.InvariantCulture);
            clr.ScB = float.Parse(xx[2], CultureInfo.InvariantCulture);
          }
          else if (xx.Length == 4)
          {
            clr.ScA = float.Parse(xx[0], CultureInfo.InvariantCulture);
            clr.ScR = float.Parse(xx[1], CultureInfo.InvariantCulture);
            clr.ScG = float.Parse(xx[2], CultureInfo.InvariantCulture);
            clr.ScB = float.Parse(xx[3], CultureInfo.InvariantCulture);
          }
          else
            throw new NotImplementedException("Color type format.");
        }
        else if (value.StartsWith("ContextColor"))
        {
          DevHelper.NotImplemented("Color profile: " + value);
          // HACK: just continue
          return FromArgb(255, 0, 128, 0);
        }
        else
          throw new NotImplementedException("Color type.");
      }
      return clr;
    }

    ColorType colorType;
    SColor sRgbColor;
    SCColor scRgbColor;
  }
}