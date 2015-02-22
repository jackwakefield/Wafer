using Wafer.Core.Exceptions;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Wafer.Core {
    public class Dimension {
        public int Value { get; set; }

        public DimensionUnit Unit { get; set; }

        public static Dimension Parse(string text) {
            var finalNumber = text.LastIndexOfAny(new char[] {
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
            });

            var valueText = text.Substring(0, finalNumber + 1);
            var suffixText = text.Substring(finalNumber + 1);

            int value;

            if (!Int32.TryParse(valueText, out value)) {
                throw new InvalidDimensionException(text);
            }

            return new Dimension {
                Unit = SuffixToUnit(suffixText),
                Value = value
            };
        }

        private static DimensionUnit SuffixToUnit(string suffix) {
            var dimensions = Enum.GetValues(typeof(DimensionUnit)).Cast<DimensionUnit>().ToList();

            foreach (var dimension in dimensions) {
                var memberInfo = typeof(DimensionUnit).GetMember(dimension.ToString());

                if (memberInfo.Length == 0) {
                    continue;
                }

                var attributes = memberInfo[0].GetCustomAttributes(typeof(DimensionSuffixAttribute), false);

                for (var j = 0; j < attributes.Length; j++) {
                    var attribute = attributes[j] as DimensionSuffixAttribute;

                    if (attribute != null && attribute.Value == suffix) {
                        return dimension;
                    }
                }
            }

            throw new InvalidDimensionUnitException(suffix);
        }
    }
}
