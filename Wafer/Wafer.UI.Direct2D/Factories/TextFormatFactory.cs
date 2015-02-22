using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using SharpDX.DirectWrite;
using Factory = SharpDX.DirectWrite.Factory;

namespace Wafer.UI.Direct2D.Factories {
    public class TextFormatFactory : ITextFormatFactory {
        private readonly IDictionary<string, TextFormat> items;
        private readonly IDirectWriteFactoryProvider directWriteFactoryProvider;

        public TextFormatFactory(IDirectWriteFactoryProvider directWriteFactoryProvider) {
            items = new Dictionary<string, TextFormat>();
            this.directWriteFactoryProvider = directWriteFactoryProvider;
        }

        public TextFormat CreateTextFormat(string fontFamilyName, float fontSize) {
            var directWriteFactory = directWriteFactoryProvider.DirectWriteFactory;

            if (directWriteFactory == null || directWriteFactory.IsDisposed) {
                // TODO: throw exception
                return null;
            }

            var key = CreateKeyName(fontFamilyName, fontSize);
            TextFormat textFormat;

            if (items.TryGetValue(key, out textFormat)) {
                return textFormat;
            }

            textFormat = new TextFormat(directWriteFactory, fontFamilyName, fontSize);
            items.Add(key, textFormat);

            return textFormat;
        }

        public void DisposeItems() {
            foreach (var key in items.Keys.ToList()) {
                var brush = items[key];
                Utilities.Dispose(ref brush);

                items.Remove(key);
            }
        }

        private static string CreateKeyName(string fontFamilyName, float fontSize) {
            return string.Format("{0} * {1}", fontFamilyName, fontSize);
        }
    }
}
