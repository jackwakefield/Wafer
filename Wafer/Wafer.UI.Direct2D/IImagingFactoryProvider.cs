using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.WIC;

namespace Wafer.UI.Direct2D {
    public interface IImagingFactoryProvider {
        ImagingFactory ImagingFactory { get; }
    }
}
