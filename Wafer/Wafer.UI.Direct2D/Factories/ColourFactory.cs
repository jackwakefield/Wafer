using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafer.Core;
using SharpDX;
using SharpDX.Direct2D1;

namespace Wafer.UI.Direct2D.Factories {
    public class ColourFactory : IColourFactory {
        private readonly IDictionary<int, SolidColorBrush> solidColorBrushes;
        private readonly IRenderTargetProvider renderTargetProvider;

        public ColourFactory(IRenderTargetProvider renderTargetProvider) {
            solidColorBrushes = new Dictionary<int, SolidColorBrush>();
            this.renderTargetProvider = renderTargetProvider;
        }

        public SolidColorBrush CreateSolidColourBrush(Colour colour) {
            if (colour == null) {
                return null;
            }

            var renderTarget = renderTargetProvider.RenderTarget;

            if (renderTarget == null || renderTarget.IsDisposed) {
                // TODO: throw exception
                return null;
            }

            var argb = colour.Argb;
            SolidColorBrush brush;

            if (solidColorBrushes.TryGetValue(argb, out brush)) {
                return brush;
            }

            brush = new SolidColorBrush(renderTarget, new Color4(argb));
            solidColorBrushes.Add(argb, brush);

            return brush;
        }

        public void DisposeItems() {
            foreach (var key in solidColorBrushes.Keys) {
                var brush = solidColorBrushes[key];
                Utilities.Dispose(ref brush);

                solidColorBrushes.Remove(key);
            }
        }
    }
}
