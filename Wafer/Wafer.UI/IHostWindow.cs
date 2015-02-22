using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafer.UI.Views;

namespace Wafer.UI {
    public interface IHostWindow {
        void Run();

        void AddChild(IView view);

        void RemoveChild(IView view);
    }
}
