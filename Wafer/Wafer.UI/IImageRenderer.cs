using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.UI {
    public interface IImageRenderer {
        void Draw(IImage image, float x, float y, float width, float height);
    }
}
