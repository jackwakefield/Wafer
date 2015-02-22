using Wafer.Core.Exceptions;
using System;
using System.Globalization;

namespace Wafer.Core {
    public class Colour {
        public static Colour Transparent { get; private set; }

        public static Colour White { get; private set; }

        public int Alpha {
            get { return (Argb >> 24) & 0xFF; }
        }

        public int Red {
            get { return (Argb >> 16) & 0xFF; }
        }

        public int Green {
            get { return (Argb >> 8) & 0xFF; }
        }

        public int Blue {
            get { return Argb & 0xFF; }
        }

        public int Argb { get; private set; }

        static Colour() {
            Transparent = FromArgb(0, 0, 0, 0);
            White = FromRgb(255, 255, 255);
        }

        private Colour(int argb) {
            Argb = argb;
        }
        
        public static Colour FromString(string value) {
            var colour = value.Replace("#", "");

            if (colour.Length == 6) {
                colour = "FF" + colour;
            }

            if (colour.Length != 8) {
                throw new InvalidColourFormatException(value);
            }

            int argb;

            if (!Int32.TryParse(colour, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out argb)) {
                throw new InvalidColourFormatException(value);
            }

            return new Colour(argb);
        }

        public static Colour FromArgb(int alpha, int red, int blue, int green) {
            return new Colour(((alpha & 0xFF) << 24) | ((red & 0xFF) << 16) | ((green & 0xFF) << 8) | (blue & 0xFF));
        }

        public static Colour FromRgb(int red, int blue, int green) {
            return FromArgb(255, red, blue, green);
        }
    }
}
