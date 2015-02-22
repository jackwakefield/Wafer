using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.DirectWrite;

namespace Wafer.UI.Direct2D.Factories {
    public class TextLayoutFactory : ITextLayoutFactory {
        private readonly List<WeakReference<IDisposable>> items;
        private readonly IDirectWriteFactoryProvider directWriteFactoryProvider;

        public TextLayoutFactory(IDirectWriteFactoryProvider directWriteFactoryProvider) {
            items = new List<WeakReference<IDisposable>>();
            this.directWriteFactoryProvider = directWriteFactoryProvider;
        }

        public TextLayout CreateTextLayout(TextFormat format, string text, int maximumWidth, int maximumHeight) {
            var directWriteFactory = directWriteFactoryProvider.DirectWriteFactory;

            if (directWriteFactory == null || directWriteFactory.IsDisposed) {
                // TODO: throw exception
                return null;
            }

            // TODO: caching

            return new TextLayout(directWriteFactory, text, format, maximumWidth, maximumHeight);
        }

        public void DisposeItems() {
            foreach (var reference in items.ToList()) {
                IDisposable item;

                if (reference.TryGetTarget(out item)) {
                    Utilities.Dispose(ref item);
                }

                items.Remove(reference);
            }
        }
    }
}
