using System.Windows.Forms;
using Wafer.UI.Views;
using System;
using System.Globalization;
using Timer = System.Timers.Timer;

namespace Wafer.StatusBar {
    [ViewName("BatteryImageView")]
    public class BatteryImageView : ImageView {
        private const int TimerInterval = 1000;

        public BatteryImageView() {
            var batteryTimer = new Timer(TimerInterval);
            batteryTimer.Elapsed += (sender, args) => SetBatteryImage();
            batteryTimer.Enabled = true;

            SetBatteryImage();
        }

        private void SetBatteryImage() {
            var status = SystemInformation.PowerStatus;
            var image = "Icons/battery_unknown_white.png";

            if (status.BatteryChargeStatus.HasFlag(BatteryChargeStatus.Charging)
                    || status.PowerLineStatus.HasFlag(PowerLineStatus.Online)) {
                if (status.BatteryLifePercent < 0.3) {
                    image = "Icons/battery_charging_20_white.png";
                } else if (status.BatteryLifePercent < 0.5) {
                    image = "Icons/battery_charging_30_white.png";
                } else if (status.BatteryLifePercent < 0.6) {
                    image = "Icons/battery_charging_50_white.png";
                } else if (status.BatteryLifePercent < 0.8) {
                    image = "Icons/battery_charging_60_white.png";
                } else if (status.BatteryLifePercent < 0.9) {
                    image = "Icons/battery_charging_80_white.png";
                } else if (status.BatteryLifePercent < 0.95) {
                    image = "Icons/battery_charging_90_white.png";
                } else {
                    image = "Icons/battery_charging_full_white.png";
                }
            } else if (!status.BatteryChargeStatus.HasFlag(BatteryChargeStatus.NoSystemBattery)
                    && !status.BatteryChargeStatus.HasFlag(BatteryChargeStatus.Unknown)) {
                if (status.BatteryLifePercent < 0.2) {
                    image = "Icons/battery_alert_white.png";
                } else if (status.BatteryLifePercent < 0.3) {
                    image = "Icons/battery_20_white.png";
                } else if (status.BatteryLifePercent < 0.5) {
                    image = "Icons/battery_30_white.png";
                } else if (status.BatteryLifePercent < 0.6) {
                    image = "Icons/battery_50_white.png";
                } else if (status.BatteryLifePercent < 0.8) {
                    image = "Icons/battery_60_white.png";
                } else if (status.BatteryLifePercent < 0.9) {
                    image = "Icons/battery_80_white.png";
                } else if (status.BatteryLifePercent < 0.95) {
                    image = "Icons/battery_90_white.png";
                } else {
                    image = "Icons/battery_full_white.png";
                }
            }

            ImageSource = image;
        }
    }
}
