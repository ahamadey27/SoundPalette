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
            // TODO: Implement mapping from hue to pitch class.
            throw new NotImplementedException();
        }

        public string GetCombinedModeAndExtension(int saturation, int lightness)
        {
            // TODO: Implement logic for determining chord mode and extension.
            throw new NotImplementedException();
        }


    }
}