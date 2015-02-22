using Newtonsoft.Json;
using Wafer.Core;
using System;

namespace Wafer.Utils.Resources.Json.Converters {
    public class DimensionConverter : JsonConverter {
        private readonly IResourceService resources;

        public DimensionConverter(IResourceService resources) {
            this.resources = resources;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }

            var value = (string)reader.Value;

            if (resources.IsResourceIdentifier(value)) {
                return resources.GetDimensionByIdentifier(value);
            }

            return Dimension.Parse(value);
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(Dimension);
        }
    }
}
