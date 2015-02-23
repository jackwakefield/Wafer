using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Wafer.UI.Views;

namespace Wafer.StatusBar {
    [ViewName("ClockView")]
    public class ClockView : TextView {
        private const int ClockInterval = 1000;

        public ClockView() {
            var clockTimer = new Timer(ClockInterval);
            clockTimer.Elapsed += (sender, args) => SetTime();
            clockTimer.Enabled = true;

            SetTime();
        }

        private void SetTime() {
            Text = string.Format("{0:HH:mm}", DateTime.Now);
        }
    }
}
