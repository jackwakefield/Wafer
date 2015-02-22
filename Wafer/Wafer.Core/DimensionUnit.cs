using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.Core {
    public enum DimensionUnit {
        [DimensionSuffix("dp")]
        DensityIndependentPixels,

        [DimensionSuffix("sp")]
        ScaleIndependentPixels,

        [DimensionSuffix("px")]
        Pixels
    }
}
