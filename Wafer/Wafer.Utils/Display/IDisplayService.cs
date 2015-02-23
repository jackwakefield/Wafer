using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.Utils.Display {
    public interface IDisplayService {
        float DiagonalSize { get; }

        int Width { get; }

        int Height { get; }

        float DisplayRatio { get; }

        float PixelsPerInch { get; }

        float CalculatePixelsPerInch(float value);
    }
}
