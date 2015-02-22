using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;

namespace Wafer.UI.Direct2D {
    public interface IRenderTargetProvider {
        RenderTarget RenderTarget { get; }
    }
}
