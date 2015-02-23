using System.Runtime.Remoting.Contexts;
using Wafer.Core;

namespace Wafer.UI.Views {
    public interface IView {
        IView Parent { get; set; }

        float ActualWidth { get; }

        float ActualHeight { get; }

        Point ActualPosition { get; }

        void SetContext(IContext context);

        void Draw();

        void OnResize();
    }
}
