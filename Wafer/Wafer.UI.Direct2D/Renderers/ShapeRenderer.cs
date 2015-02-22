using Wafer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafer.UI.Direct2D.Factories;
using SharpDX;

namespace Wafer.UI.Direct2D.Renderers {
    public class ShapeRenderer : IShapeRenderer {
        private readonly IRenderTargetProvider renderTargetProvider;
        private readonly IColourFactory colourFactory;
        
        public ShapeRenderer(IRenderTargetProvider renderTargetProvider, IColourFactory colourFactory) {
            this.renderTargetProvider = renderTargetProvider;
            this.colourFactory = colourFactory;
        }

        public void FillRectangle(int x, int y, int width, int height, Colour colour) {
            var renderTarget = renderTargetProvider.RenderTarget;

            if (renderTarget != null && !renderTarget.IsDisposed) {
                var brush = colourFactory.CreateSolidColourBrush(colour);
                renderTarget.FillRectangle(new RectangleF(x, y, width, height), brush);
            }
        }
    }
}
