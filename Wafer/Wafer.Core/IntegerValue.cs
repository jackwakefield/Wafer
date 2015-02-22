using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.Core {
    public class IntegerValue {
        private readonly int value;

        public IntegerValue(int value) {
            this.value = value;
        }

        public int ToInt() {
            return value;
        }

        public static implicit operator int(IntegerValue value) {
            return value.ToInt();
        }

        public static implicit operator IntegerValue(int value) {
            return new IntegerValue(value);
        }
    }
}
