using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.Utils.Resources.Exceptions {
    public class UnknownResourceException : Exception {
        public UnknownResourceException(string type, string name)
            : base(string.Format("Unknown '{0}' resource value with the name '{1}'", type, name)) {
        }
    }
}
