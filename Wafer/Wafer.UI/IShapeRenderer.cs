using Wafer.Core;

namespace Wafer.UI {
    public interface IShapeRenderer {
        void FillRectangle(int x, int y, int width, int height, Colour colour);
    }
}
