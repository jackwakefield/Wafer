using SharpDX.Direct2D1;

namespace Wafer.UI.Direct2D {
    public class Image : IImage {
        public int Width {
            get {
                if (Bitmap != null) {
                    return Bitmap.PixelSize.Width;
                }

                return 0;
            }
        }

        public int Height {
            get {
                if (Bitmap != null) {
                    return Bitmap.PixelSize.Height;
                }

                return 0;
            }
        }

        public Bitmap Bitmap { get; set; }

        public Image(Bitmap bitmap) {
            Bitmap = bitmap;
        }
    }
}
