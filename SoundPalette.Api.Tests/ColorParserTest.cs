using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using SoundPalette.Api.Models;
using Xunit;
using static SoundPalette.Api.Models.HslColorModel;

namespace SoundPalette.Api.Tests
{
    public class ColorParserTest
    {
        [Fact]
        public void ParseToHsl_ValidHex_ReturnsExpectedHsl()
        {
            //Arrange: Setup sample HEX and the expected HSL result
            var hex = "#3A9FD9"; //Blue
            var expectedHsl = new HslColorModel(204, 66, 54);

            var parser = new ColorParser(); // You will implement this class later

            // Act: Convert the HEX color to HSL.
            var result = parser.ParseToHsl(hex);

            // Assert: The result should match the expected HSL values:
            AssertHslClose(expectedHsl, result);
        }

        [Fact]
        public void ParseToHsl_BlackHex_ReturnsBlackHsl()
        {
            // Arrange: Black HEX should map to HSL (0, 0, 0)
            var parser = new ColorParser();
            var result = parser.ParseToHsl("#000000");
            Assert.Equal(new HslColorModel(0, 0, 0), result);
        }

        [Fact]
        public void ParseToHsl_WhiteHex_ReturnsWhiteHsl()
        {
            // Arrange: White HEX should map to HSL (0, 0, 100)
            var parser = new ColorParser();
            var result = parser.ParseToHsl("#FFFFFF");
            Assert.Equal(new HslColorModel(0, 0, 100), result);
        }

        [Theory]
        [InlineData("#ZZZZZZ")]
        [InlineData("#12345")]
        [InlineData("")]
        [InlineData(null)]
        public void ParseToHsl_InvalidHex_ThrowsArgumentException(string hex)
        {
            // Arrange: Invalid HEX should throw an exception
            var parser = new ColorParser();
            Assert.Throws<ArgumentException>(() => parser.ParseToHsl(hex));
        }

        private void AssertHslClose(HslColorModel expected, HslColorModel actual, int tolerance = 2)
        {
            Assert.InRange(actual.H, expected.H - tolerance, expected.H + tolerance);
            Assert.InRange(actual.S, expected.S - tolerance, expected.S + tolerance);
            Assert.InRange(actual.L, expected.L - tolerance, expected.L + tolerance);
        }
    }

    public class ColorParser
    {
        public HslColorModel ParseToHsl(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                throw new ArgumentException("Hex string cannot be null or empty.", nameof(hex));
            }

            // Validate HEX format (#RRGGBB or RRGGBB)
            var hexPattern = @"^#?([0-9a-fA-F]{6})$";
            var match = Regex.Match(hex, hexPattern);
            if (!match.Success)
            {
                throw new ArgumentException("Invalid HEX color format. Expected #RRGGBB or RRGGBB.", nameof(hex));
            }

            string hexValue = match.Groups[1].Value;


            // Convert HEX to RGB
            int r = int.Parse(hexValue.Substring(0, 2), NumberStyles.HexNumber);
            int g = int.Parse(hexValue.Substring(2, 2), NumberStyles.HexNumber);
            int b = int.Parse(hexValue.Substring(4, 2), NumberStyles.HexNumber);

            // Normalize RGB values to be between 0 and 1
            double r_normalized = r / 255.0;
            double g_normalized = g / 255.0;
            double b_normalized = b / 255.0;

            // Find min and max RGB values
            double max = Math.Max(r_normalized, Math.Max(g_normalized, b_normalized));
            double min = Math.Min(r_normalized, Math.Min(g_normalized, b_normalized));
            double delta = max - min;

            // Calculate Luminance (L)
            double l = (max + min) / 2.0;

            double h = 0, s = 0;

            if (delta != 0) // Not a grayscale color, so calculate H and S
            {
                // Calculate Saturation (S)
                s = (l < 0.5) ? (delta / (max + min)) : (delta / (2.0 - max - min));

                // Calculate Hue (H)
                if (max == r_normalized)
                {
                    h = (g_normalized - b_normalized) / delta;
                    if (g_normalized < b_normalized)
                    {
                        h += 6;
                    }
                }
                else if (max == g_normalized)
                {
                    h = 2.0 + (b_normalized - r_normalized) / delta;
                }
                else // max == b_normalized
                {
                    h = 4.0 + (r_normalized - g_normalized) / delta;
                }

                h *= 60; // Convert to degrees
            }

            // Return HSL, scaling S and L to be percentages, H in degrees
            return new HslColorModel((int)Math.Round(h), (int)Math.Round(s * 100), (int)Math.Round(l * 100));
        }
    }
}