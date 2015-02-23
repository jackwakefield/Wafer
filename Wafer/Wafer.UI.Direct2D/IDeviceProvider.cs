using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Direct3D = SharpDX.Direct3D10;

namespace Wafer.UI.Direct2D {
    public interface IDeviceProvider {
        Direct3D.Device1 Device { get; }
    }
}
