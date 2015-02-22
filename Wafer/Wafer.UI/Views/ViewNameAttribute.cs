using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.UI.Views {
    public class ViewNameAttribute : Attribute {
        public string Name { get; set; }

        public ViewNameAttribute(string name) {
            Name = name;
        }
    }
}
