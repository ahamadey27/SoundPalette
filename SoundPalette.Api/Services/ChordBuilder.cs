using System;
using System.Collections.Generic;
using System.Linq;

namespace SoundPalette.Api.Services
{
    public class ChordBuilder : IChordBuilder
    {
        // Maps pitch class names to base MIDI note numbers (C4 = 60)
        private static readonly Dictionary<string, int> PitchClassToMidi = new()
        {
            { "C", 60 }, { "C#", 61 }, { "D", 62 }, { "D#", 63 }, { "E", 64 }, { "F", 65 },
            { "F#", 66 }, { "G", 67 }, { "G#", 68 }, { "A", 69 }, { "A#", 70 }, { "B", 71 }
        };

        public (int[] midiNotes, double[] frequencies) BuildMusicalProperties(string pitchClass, string combinedModeAndExtension)
        {
            // Determine chord intervals based on mode/extension
            var intervals = new List<int> { 0, 4, 7 }; // Major triad by default
            if (combinedModeAndExtension.StartsWith("minor"))
            {
                intervals = new List<int> { 0, 3, 7 };
            }
            if (combinedModeAndExtension.Contains("7"))
            {
                intervals.Add(10); // Add minor 7th
            }

            int baseMidi = PitchClassToMidi.ContainsKey(pitchClass) ? PitchClassToMidi[pitchClass] : 60;
            var midiNotes = intervals.Select(i => baseMidi + i).ToArray();
            var frequencies = midiNotes.Select(MidiToFrequency).ToArray();
            return (midiNotes, frequencies);
        }

        // Converts MIDI note number to frequency in Hz
        private double MidiToFrequency(int midiNote)
        {
            return 440.0 * Math.Pow(2, (midiNote - 69) / 12.0);
        }
    }
}
