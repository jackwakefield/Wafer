using System;

namespace Wafer.Core.Exceptions {
    public class InvalidDimensionException : Exception {
        public InvalidDimensionException(string value)
            : base(string.Format("'{0}' is an invalid dimension", value)) {
        }
    }
}
