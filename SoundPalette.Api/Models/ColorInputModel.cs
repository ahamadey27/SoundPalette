using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundPalette.Api.Models
{
    // Represents the input payload for the /convert endpoint.lea
    // This record holds the color value as a string (e.g., "#3A9FD9").
    public record ColorInputModel(string ColorValue);
}