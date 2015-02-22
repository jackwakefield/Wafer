using System;

namespace Wafer.Core.Exceptions {
    public class InvalidDimensionUnitException : Exception {
        public InvalidDimensionUnitException(string value)
            : base(string.Format("'{0}' is an invalid dimension unit", value)) {
        }
    }
}
