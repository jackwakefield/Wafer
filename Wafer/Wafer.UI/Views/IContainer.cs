using System.Collections.Generic;

namespace Wafer.UI.Views {
    public interface IContainer {
        IList<IView> Children { get; } 
    }
}
