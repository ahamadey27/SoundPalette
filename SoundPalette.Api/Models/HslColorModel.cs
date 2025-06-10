using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundPalette.Api.Models
{
    public class HslColorModel
    {
        // Represents a color in HSL (Hue, Saturation, Lightness) format.
        // Used internally after parsing and converting the input color.
        public record HslColor(double H, double S, double L);

    }
}