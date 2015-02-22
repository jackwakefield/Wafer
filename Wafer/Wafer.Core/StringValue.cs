using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.Core {
    public class StringValue {
        private readonly string value;
        
        public StringValue(string value) {
            this.value = value;
        }

        public static implicit operator string(StringValue value) {
            return value.ToString();
        }

        public static implicit operator StringValue(string value) {
            return new StringValue(value);
        }
    }
}
