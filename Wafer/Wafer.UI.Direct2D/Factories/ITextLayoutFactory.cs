using SharpDX.DirectWrite;

namespace Wafer.UI.Direct2D.Factories {
    public interface ITextLayoutFactory {
        TextLayout CreateTextLayout(TextFormat format, string text, float maximumWidth, float maximumHeight);

        void DisposeItems();
    }
}