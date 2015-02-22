using System.Globalization;
using Newtonsoft.Json;
using Wafer.Core;
using System;

namespace Wafer.Utils.Resources.Json.Converters {
    public class ColourConverter : JsonConverter {
        private readonly IResourceService resources;
        
        public ColourConverter(IResourceService resources) {
            this.resources = resources;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }

            if (reader.TokenType != JsonToken.String) {
                throw new Exception(string.Format("Unexpected token parsing colour. Expected String, got {0}.", reader.TokenType));
            }

            var value = (string)reader.Value;

            if (resources.IsResourceIdentifier(value)) {
                return resources.GetColourByIdentifier(value);
            }

            return Colour.FromString(value);
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof (Colour);
        }
    }
}
