using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundPalette.Api.Services
{
    public class MusicTheoryService : IMusicTheoryService
    {
        public string GetPitchClassFromHue(int hue)
        {
            // Map hue (0-360) to one of 12 pitch classes.
            // Each pitch class covers a 30-degree segment.
            string[] pitchClasses = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            int index = ((int)Math.Round(hue / 30.0)) % 12;
            return pitchClasses[index];
        }

        public string GetCombinedModeAndExtension(int saturation, int lightness)
        {
            // Example logic: Major if saturation >= 50, minor otherwise; 7th if lightness >= 50, triad otherwise.
            string mode = saturation >= 50 ? "major" : "minor";
            string extension = lightness >= 50 ? "7" : "";
            return string.IsNullOrEmpty(extension) ? mode : $"{mode}-{extension}";
        }
    }
}