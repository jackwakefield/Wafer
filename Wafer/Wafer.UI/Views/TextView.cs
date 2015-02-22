using System;
using System.Net.Mime;
using System.Runtime.Remoting.Contexts;
using Wafer.Core;

namespace Wafer.UI.Views {
    [ViewName("TextView")]
    public class TextView : View {
        public const string DefaultFontFamily = "Calibri";
        public static Dimension DefaultFontSize;

        public string Text {
            get { return text; }
            set {
                text = value;
                InvalidateSize();
            }
        }

        public Colour TextColour { get; set; }

        private string text;

        static TextView() {
            DefaultFontSize = new Dimension {Value = 14, Unit = DimensionUnit.ScaleIndependentPixels};
        }

        public TextView() {
            TextColour = Colour.White;
        }

        public override void Draw() {
            if (context == null) {
                return;
            }

            base.Draw();

            if (parent != null) {
                var width = Width != null ? Width.Value : parent.ActualWidth;
                var height = Height != null ? Height.Value : parent.ActualHeight;

                context.TextRenderer.Draw(text, ActualPosition.X, ActualPosition.Y, width, height,
                    DefaultFontFamily, DefaultFontSize, TextColour);   
            }
        }


        public override void InvalidateSize() {
            ActualWidth = 0;
            ActualHeight = 0;

            base.InvalidateSize();

            if (context != null) {
                var width = Width != null ? Width.Value : parent.ActualWidth;
                var height = Height != null ? Height.Value : parent.ActualHeight;

                if (ActualWidth == 0) {
                    ActualWidth = context.TextRenderer.MeasureWidth(text, width, height, DefaultFontFamily, DefaultFontSize);
                }

                if (ActualHeight == 0) {
                    ActualHeight = context.TextRenderer.MeasureHeight(text, width, height, DefaultFontFamily, DefaultFontSize);
                }
            }

            InvalidateLocation();
        }
    }
}
