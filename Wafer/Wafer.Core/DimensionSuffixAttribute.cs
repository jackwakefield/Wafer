using System;

namespace Wafer.Core {
    public class DimensionSuffixAttribute : Attribute {
        public string Value { get; set; }

        public DimensionSuffixAttribute(string value) {
            Value = value;
        }
    }
}
