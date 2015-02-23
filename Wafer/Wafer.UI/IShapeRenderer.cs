using Wafer.Core;

namespace Wafer.UI {
    public interface IShapeRenderer {
        void FillRectangle(float x, float y, float width, float height, Colour colour);
    }
}
