using System.Drawing;
using Newtonsoft.Json;
using Wafer.Core;
using Point = Wafer.Core.Point;

namespace Wafer.UI.Views {
    public abstract class View : IView {
        [JsonIgnore]
        public float ActualWidth { get; protected set; }

        [JsonIgnore]
        public float ActualHeight { get; protected set; }

        [JsonIgnore]
        public Point ActualPosition { get; protected set; }

        [JsonIgnore]
        public IView Parent {
            get { return parent; }
            set {
                parent = value;
                InvalidateSize();
            }
        }

        public bool FillVertical {
            get { return fillVertical; }
            set {
                fillVertical = value;
                InvalidateSize();
            }
        }

        public bool FillHorizontal {
            get { return fillHorizontal; }
            set {
                fillHorizontal = value;
                InvalidateSize();
            }
        }
        
        public bool CenterVertical {
            get { return centreVertical; }
            set {
                centreVertical = value;
                InvalidateLocation();
            }
        }

        public bool CenterHorizontal {
            get { return centreHorizontal; }
            set {
                centreHorizontal = value;
                InvalidateLocation();
            }
        }

        public Dimension Width {
            get { return width; }
            set {
                width = value;
                InvalidateSize();
            }
        }

        public Dimension Height {
            get { return height; }
            set {
                height = value;
                InvalidateSize();
            }
        }

        public Colour BackgroundColour { get; set; }

        protected IContext context;
        protected IView parent;

        protected bool fillVertical;
        protected bool fillHorizontal;
        protected bool centreVertical;
        protected bool centreHorizontal;
        protected Dimension width;
        protected Dimension height;

        protected View() {
            ActualPosition = new Point();
            BackgroundColour = Colour.Transparent;
        }

        public virtual void Draw() {
            if (context == null) {
                return;
            }

            context.ShapeRenderer.FillRectangle(ActualPosition.X, ActualPosition.Y, ActualWidth, ActualHeight,
                BackgroundColour);
        }

        public virtual void OnResize() {
            InvalidateSize();
            InvalidateLocation();
        }

        public virtual void InvalidateSize() {
            if (context == null) {
                return;
            }

            if (Parent != null && FillVertical) {
                ActualHeight = Parent.ActualHeight;
            } else if (Height != null) {
                ActualHeight = Height.Value;

                if (Height.Unit == DimensionUnit.DensityIndependentPixels) {
                    ActualHeight = context.Display.CalculatePixelsPerInch(ActualHeight);
                }
            } else {
                ActualHeight = 0;
            }

            if (Parent != null && FillHorizontal) {
                ActualWidth = Parent.ActualWidth;
            } else if (Width != null) {
                ActualWidth = Width.Value;

                if (Width.Unit == DimensionUnit.DensityIndependentPixels) {
                    ActualWidth = context.Display.CalculatePixelsPerInch(ActualWidth);
                }
            } else {
                ActualWidth = 0;
            }
        }

        public virtual void InvalidateLocation() {
            if (CenterHorizontal && parent != null) {
                ActualPosition.X = parent.ActualWidth/2 - ActualWidth/2;
            } else {
                ActualPosition.X = 0;
            }

            if (CenterVertical && parent != null) {
                ActualPosition.Y = parent.ActualHeight/2 - ActualHeight/2;
            } else {
                ActualPosition.Y = 0;
            }
        }

        public virtual void SetContext(IContext context) {
            this.context = context;
        }
    }
}