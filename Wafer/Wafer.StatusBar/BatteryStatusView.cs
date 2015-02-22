using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafer.UI.Views;

namespace Wafer.StatusBar {
    [ViewName("BatteryIconView")]
    public class BatteryIconView : TextView {
        public BatteryIconView() {
            Text = "Battery";
        }
    }
}
