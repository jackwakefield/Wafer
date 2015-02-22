using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.Utils.Resources.Exceptions {
    public class DuplicateValueNameException : Exception {
        public DuplicateValueNameException(string type, string name)
            : base(string.Format("Duplicate '{0}' resource value with the name '{1}'", type, name)) {
        }
    }
}
