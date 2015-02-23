using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafer.Core;

namespace Wafer.UI {
    public interface ITextRenderer {
        void Draw(string text, float x, float y, float maximumWidth, float maximumHeight, string fontFamily,
            float fontSize, Colour colour);

        int MeasureWidth(string text, float maximumWidth, float maximumHeight, string fontFamily, float fontSize);

        int MeasureHeight(string text, float maximumWidth, float maximumHeight, string fontFamily, float fontSize);

        Size Measure(string text, float maximumWidth, float maximumHeight, string fontFamily, float fontSize);
    }
}
