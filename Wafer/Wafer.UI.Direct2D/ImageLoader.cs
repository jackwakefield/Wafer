using SharpDX;
using SharpDX.IO;
using SharpDX.WIC;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wafer.Utils.Config;
using Bitmap = SharpDX.Direct2D1.Bitmap;

namespace Wafer.UI.Direct2D {
    public class ImageLoader : IImageLoader {
        private readonly IConfigService config;
        private readonly IRenderTargetProvider renderTargetProvider;
        private readonly IImagingFactoryProvider imagingFactoryProvider;

        private readonly IDictionary<string, Image> images;

        public ImageLoader(IConfigService config, IRenderTargetProvider renderTargetProvider, IImagingFactoryProvider imagingFactoryProvider) {
            this.config = config;
            this.renderTargetProvider = renderTargetProvider;
            this.imagingFactoryProvider = imagingFactoryProvider;

            images = new Dictionary<string, Image>();
        }

        public IImage Load(string path) {
            var renderTarget = renderTargetProvider.RenderTarget;

            if (renderTarget == null || renderTarget.IsDisposed) {
                return null; // TODO: throw exception
            }

            if (!File.Exists(path)) {
                // TODO: improve absolute/relative path handling
                path = Path.Combine(config.Paths.Resources, path);
            }
    
            var bitmap = CreateBitmap(path);
            var image = new Image(bitmap);

            images.Add(path, image);

            return image;
        }

        private Bitmap CreateBitmap(string path) {
            var renderTarget = renderTargetProvider.RenderTarget;
            var imagingFactory = imagingFactoryProvider.ImagingFactory;

            if (renderTarget == null || renderTarget.IsDisposed || imagingFactory == null || imagingFactory.IsDisposed) {
                return null; // TODO: throw exception
            }

            var fileStream = new NativeFileStream(path, NativeFileMode.Open, NativeFileAccess.Read);
            var bitmapDecoder = new BitmapDecoder(imagingFactory, fileStream, DecodeOptions.CacheOnDemand);
            var frame = bitmapDecoder.GetFrame(0);
            
            var converter = new FormatConverter(imagingFactory);
            converter.Initialize(frame, SharpDX.WIC.PixelFormat.Format32bppPRGBA);

            var bitmap = Bitmap.FromWicBitmap(renderTarget, converter);

            Utilities.Dispose(ref bitmapDecoder);
            Utilities.Dispose(ref fileStream);

            return bitmap;
        }

        public void DisposeAll() {
            foreach (var key in images.Keys.ToList()) {
                var image = images[key];

                var bitmap = image.Bitmap;
                Utilities.Dispose(ref bitmap);
                image.Bitmap = null;
            }
        }

        public void ReloadAll() {
            foreach (var key in images.Keys.ToList()) {
                var image = images[key];
                image.Bitmap = CreateBitmap(key);
            }
        }
    }
}
