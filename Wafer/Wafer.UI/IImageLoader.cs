using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.UI {
    public interface IImageLoader {
        IImage Load(string path);
    }
}
