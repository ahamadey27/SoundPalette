using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SoundPalette.Api.Models;

namespace SoundPalette.Api.Models
{
    public class ColorParser
    {
        //Service responsible for parsing HEX color strings and converting them to HSL.
        public HslColorModel ParseToHsl(string hex)
        {
            // Remove the '#' if present
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            // Parse RGB components from the HEX string
            int r = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            int g = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            int b = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);

            // Convert RGB to the range [0, 1]
            double rNorm = r / 255.0;
            double gNorm = g / 255.0;
            double bNorm = b / 255.0;

            // Find min and max RGB values
            double max = Math.Max(rNorm, Math.Max(gNorm, bNorm));
            double min = Math.Min(rNorm, Math.Min(gNorm, bNorm));
            double h = 0, s, l = (max + min) / 2.0;

            // Calculate Hue
            if (max == min)
            {
                h = 0; // Achromatic
            }
            else
            {
                double d = max - min;
                if (max == rNorm)
                {
                    h = (gNorm - bNorm) / d + (gNorm < bNorm ? 6 : 0);
                }
                else if (max == gNorm)
                {
                    h = (bNorm - rNorm) / d + 2;
                }
                else if (max == bNorm)
                {
                    h = (rNorm - gNorm) / d + 4;
                }
                h *= 60;
            }

            // Calculate Saturation
            if (max == min)
            {
                s = 0;
            }
            else
            {
                s = l > 0.5
                    ? (max - min) / (2.0 - max - min)
                    : (max - min) / (max + min);
            }

            // Convert to standard HSL scale
            int hInt = (int)Math.Round(h);
            int sInt = (int)Math.Round(s * 100);
            int lInt = (int)Math.Round(l * 100);

            // Return the HSL color model
            return new HslColorModel(hInt, sInt, lInt);

        }
    }
}