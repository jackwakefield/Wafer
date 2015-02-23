using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;

namespace Wafer.UI.Direct2D {
    public class ImageRenderer : IImageRenderer {
        private readonly IRenderTargetProvider renderTargetProvider;

        public ImageRenderer(IRenderTargetProvider renderTargetProvider) {
            this.renderTargetProvider = renderTargetProvider;
        }

        public void Draw(IImage source, float x, float y, float width, float height) {
            var image = source as Image;
            var renderTarget = renderTargetProvider.RenderTarget;

            if (image == null || renderTarget == null || renderTarget.IsDisposed) {
                return;
            }

            renderTarget.DrawBitmap(image.Bitmap, new RectangleF(x, y, width, height), 1.0f, BitmapInterpolationMode.Linear);
        }
    }
}
