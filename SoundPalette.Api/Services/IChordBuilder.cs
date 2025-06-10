namespace SoundPalette.Api.Services
{
    // Interface for building chords and calculating MIDI notes and frequencies.
    public interface IChordBuilder
    {
        // Builds MIDI notes and frequencies from pitch class and mode/extension.
        (int[] midiNotes, double[] frequencies) BuildMusicalProperties(string pitchClass, string combinedModeAndExtension);
    }
}
