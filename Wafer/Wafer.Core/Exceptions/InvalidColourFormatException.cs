using System;

namespace Wafer.Core.Exceptions {
    public class InvalidColourFormatException : Exception {
        public InvalidColourFormatException(string value)
            : base(string.Format("'{0}' is an invalid colour format", value)) {
        }
    }
}
