using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wafer.UI.Views {
    [ViewName("ImageView")]
    public class ImageView : View {
        public string ImageSource {
            get { return imageSource; }
            set {
                imageSource = value;
                LoadImage();
            }
        }

        private string imageSource;
        private IImage image;

        public override void Draw() {
            base.Draw();

            if (image == null) {
                return;
            }

            context.ImageRenderer.Draw(image, ActualPosition.X, ActualPosition.Y, ActualWidth, ActualHeight);
        }

        public override void InvalidateSize() {
            ActualWidth = 0;
            ActualHeight = 0;

            base.InvalidateSize();

            if (image != null) {
                if (ActualWidth != 0 && ActualHeight == 0) {
                    ActualHeight = (int)((float)ActualWidth / image.Width * image.Height);
                } else if (ActualHeight != 0 && ActualWidth == 0) {
                    ActualWidth = (int)((float)ActualHeight / image.Height * image.Width);
                } else {
                    ActualWidth = image.Width;
                    ActualHeight = image.Height;
                }
            }

            InvalidateLocation();
        }

        public override void SetContext(IContext context) {
            base.SetContext(context);
            LoadImage();
        }

        private void LoadImage() {
            if (context == null) {
                return;
            }

            image = context.ImageLoader.Load(imageSource);
            InvalidateSize();
        }
    }
}
