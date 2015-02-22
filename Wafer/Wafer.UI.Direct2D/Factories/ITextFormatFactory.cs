using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectWrite;

namespace Wafer.UI.Direct2D.Factories {
    public interface ITextFormatFactory {
        TextFormat CreateTextFormat(string fontFamilyName, float fontSize);

        void DisposeItems();
    }
}
