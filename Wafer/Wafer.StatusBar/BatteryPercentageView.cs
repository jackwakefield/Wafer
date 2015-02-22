using Wafer.UI.Views;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace Wafer.StatusBar {
    [ViewName("BatteryPercentageView")]
    public class BatteryPercentageView : TextView {
        private readonly PowerStatus status;
        
        public BatteryPercentageView() {
            status = SystemInformation.PowerStatus;
            Text = Math.Floor(status.BatteryLifePercent*100).ToString(CultureInfo.InvariantCulture) + "%";
        }
    }
}
