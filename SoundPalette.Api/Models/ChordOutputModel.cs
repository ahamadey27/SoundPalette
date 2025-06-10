using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundPalette.Api.Models
{
    // Represents the output of the chord conversion.
    public record ChordOutputModel(
        string Hex,
        HslColorModel Hsl,
        string PitchClass,
        string Mode,
        int[] MidiNotes,
        double[] FrequencyHz
    );
}