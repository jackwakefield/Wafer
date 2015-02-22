using SharpDX.DirectWrite;

namespace Wafer.UI.Direct2D.Factories {
    public interface ITextLayoutFactory {
        TextLayout CreateTextLayout(TextFormat format, string text, int maximumWidth, int maximumHeight);

        void DisposeItems();
    }
}