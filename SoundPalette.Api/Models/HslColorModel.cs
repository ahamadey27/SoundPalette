using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundPalette.Api.Models
{
    // Represents a color in HSL (Hue, Saturation, Lightness) format.
    // Used internally after parsing and converting the input color.
    public record HslColorModel(int H, int S, int L);
}