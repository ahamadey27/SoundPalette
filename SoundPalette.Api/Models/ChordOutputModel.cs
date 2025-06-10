using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundPalette.Api.Models
{
    public class ChordOutputModel
    {
        public record HslOutput(int h, int s, int l); // For nested HSL in output
        public record ChordOutput(string Hex, HslOutput Hsl, string PitchClass, string Mode, int MidiNotes, double FrequencyHz);

    }
}