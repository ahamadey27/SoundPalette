using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundPalette.Api.Services
{
    // Interface for mapping HSL values to musical properties.
    public interface IMusicTheoryService
    {
        // Maps a hue value (0-360) to a musical pitch class (e.g., C, D♭, E).
        string GetPitchClassFromHue(int hue);

        // Determines chord mode and extension (e.g., major-7) from saturation and lightness.
        string GetCombinedModeAndExtension(int saturation, int lightness);
    }
}