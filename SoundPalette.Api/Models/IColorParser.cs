namespace SoundPalette.Api.Models
{
    // Interface for parsing color strings and converting to HSL.
    public interface IColorParser
    {
        HslColorModel ParseToHsl(string hex);
    }
}
