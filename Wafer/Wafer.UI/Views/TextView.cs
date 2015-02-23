using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mime;
using System.Runtime.Remoting.Contexts;
using Wafer.Core;

namespace Wafer.UI.Views {
    [ViewName("TextView")]
    public class TextView : View {
        public const string DefaultFontFamily = "Calibri";
        public static Dimension DefaultFontSize;

        public float ActualFontSize {
            get {
                var fontSize = DefaultFontSize;
                float value = fontSize.Value;

                if (fontSize.Unit == DimensionUnit.DensityIndependentPixels ||
                    fontSize.Unit == DimensionUnit.ScaleIndependentPixels) {
                    value = context.Display.CalculatePixelsPerInch(value);
                }

                return value;
            }
        }

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
            DefaultFontSize = new Dimension {Value = 12, Unit = DimensionUnit.ScaleIndependentPixels};
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
                    DefaultFontFamily, ActualFontSize, TextColour);   
            }
        }


        public override void InvalidateSize() {
            ActualWidth = 0;
            ActualHeight = 0;

            base.InvalidateSize();

            if (context != null) {
                float width;
                float height;

                if (Width != null) {
                    width = Width.Value;

                    if (Width.Unit == DimensionUnit.DensityIndependentPixels) {
                        width = context.Display.CalculatePixelsPerInch(width);
                    }
                } else {
                    width = parent.ActualWidth;
                }

                if (Height != null) {
                    height = Height.Value;

                    if (Height.Unit == DimensionUnit.DensityIndependentPixels) {
                        height = context.Display.CalculatePixelsPerInch(height);
                    }
                } else {
                    height = parent.ActualHeight;
                }

                if (ActualWidth == 0) {
                    ActualWidth = context.TextRenderer.MeasureWidth(text, width, height, DefaultFontFamily, ActualFontSize);
                }

                if (ActualHeight == 0) {
                    ActualHeight = context.TextRenderer.MeasureHeight(text, width, height, DefaultFontFamily, ActualFontSize);
                }
            }

            InvalidateLocation();
        }
    }
}
