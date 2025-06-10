using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundPalette.Api.Models
{
    public class ColorInputModel
    {
        // Represents the input payload for the /convert endpoint.
        // This record holds the color value as a string (e.g., "#3A9FD9").
        public record ColorInput(string ColorValue);

    }
}