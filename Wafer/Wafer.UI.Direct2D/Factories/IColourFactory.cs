using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafer.Core;
using SharpDX.Direct2D1;

namespace Wafer.UI.Direct2D.Factories {

    public interface IColourFactory {
        SolidColorBrush CreateSolidColourBrush(Colour colour);

        void DisposeItems();
    }

}
