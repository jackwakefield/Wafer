namespace Wafer.UI {
    public interface IContext {
        ITextRenderer TextRenderer { get; }

        IShapeRenderer ShapeRenderer { get; }
    }
}
