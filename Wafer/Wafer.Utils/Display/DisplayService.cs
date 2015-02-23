using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.Utils.Display {
    public class DisplayService : IDisplayService {
        public float DiagonalSize {
            get { return 17.0f; }
        }

        public int Width {
            get { return 1920; }
        }

        public int Height {
            get { return 1080; }
        }


        public float DisplayRatio {
            get { return (float) Width/Height; }
        }

        public float PixelsPerInch {
            get { return (float)Math.Sqrt(Math.Pow(Width, 2) + Math.Pow(Height, 2)) / DiagonalSize; }
        }

        public float CalculatePixelsPerInch(float value) {
            return value*(PixelsPerInch/96);
        }
    }
}
