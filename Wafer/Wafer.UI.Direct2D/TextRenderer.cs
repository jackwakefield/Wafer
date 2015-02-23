using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafer.Core;
using Wafer.UI.Direct2D.Factories;
using SharpDX;

namespace Wafer.UI.Direct2D {
    public class TextRenderer : ITextRenderer {
        private readonly IRenderTargetProvider renderTargetProvider;
        private readonly IColourFactory colourFactory;
        private readonly ITextFormatFactory textFormatFactory;
        private readonly ITextLayoutFactory textLayoutFactory;

        public TextRenderer(IRenderTargetProvider renderTargetProvider, IColourFactory colourFactory,
                ITextFormatFactory textFormatFactory, ITextLayoutFactory textLayoutFactory) {
            this.renderTargetProvider = renderTargetProvider;
            this.colourFactory = colourFactory;
            this.textFormatFactory = textFormatFactory;
            this.textLayoutFactory = textLayoutFactory;
        }

        public void Draw(string text, int x, int y, int maximumWidth, int maximumHeight,
            string fontFamily, Dimension fontSize, Colour colour) {
            var renderTarget = renderTargetProvider.RenderTarget;

            if (renderTarget != null && !renderTarget.IsDisposed) {
                var textFormat = textFormatFactory.CreateTextFormat(fontFamily, fontSize.Value);
                var textLayout = textLayoutFactory.CreateTextLayout(textFormat, text, maximumWidth, maximumHeight);
                var brush = colourFactory.CreateSolidColourBrush(colour);

                if (textFormat != null && textLayout != null && brush != null) {
                    renderTarget.DrawText(text, textFormat, new RectangleF(x, y, maximumWidth, maximumHeight), brush);
                }
            }
        }

        public int MeasureWidth(string text, int maximumWidth, int maximumHeight, string fontFamily, Dimension fontSize) {
            return Measure(text, maximumWidth, maximumHeight, fontFamily, fontSize).Width;
        }

        public int MeasureHeight(string text, int maximumWidth, int maximumHeight, string fontFamily, Dimension fontSize) {
            return Measure(text, maximumWidth, maximumHeight, fontFamily, fontSize).Height;
        }

        public Size Measure(string text, int maximumWidth, int maximumHeight, string fontFamily, Dimension fontSize) {
            var textFormat = textFormatFactory.CreateTextFormat(fontFamily, fontSize.Value);
            var textLayout = textLayoutFactory.CreateTextLayout(textFormat, text, maximumWidth, maximumHeight);

            if (textFormat != null && textLayout != null) {
                return new Size((int)textLayout.Metrics.Width, (int)textLayout.Metrics.Height);
            }

            return new Size();
        }
    }
}
