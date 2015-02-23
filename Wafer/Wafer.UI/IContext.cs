using Wafer.Utils.Resources;

namespace Wafer.UI {
    public interface IContext {
        ITextRenderer TextRenderer { get; }

        IShapeRenderer ShapeRenderer { get; }

        IImageRenderer ImageRenderer { get; }

        IImageLoader ImageLoader { get; }

        IResourceService Resources { get; }
    }
}
