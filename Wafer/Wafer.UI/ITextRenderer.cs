using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafer.Core;

namespace Wafer.UI {
    public interface ITextRenderer {
        void Draw(string text, int x, int y, int maximumWidth, int maximumHeight, string fontFamily,
            Dimension fontSize, Colour colour);

        int MeasureWidth(string text, int maximumWidth, int maximumHeight, string fontFamily, Dimension fontSize);

        int MeasureHeight(string text, int maximumWidth, int maximumHeight, string fontFamily, Dimension fontSize);

        Size Measure(string text, int maximumWidth, int maximumHeight, string fontFamily, Dimension fontSize);
    }
}
