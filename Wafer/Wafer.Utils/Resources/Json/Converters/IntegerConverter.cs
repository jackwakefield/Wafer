using System.Globalization;
using Newtonsoft.Json;
using Wafer.Core;
using System;

namespace Wafer.Utils.Resources.Json.Converters {
    public class IntegerConverter : JsonConverter {
        private readonly IResourceService resources;

        public IntegerConverter(IResourceService resources) {
            this.resources = resources;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }

            int integer;

            if (reader.TokenType == JsonToken.String) {
                var value = (string) reader.Value;

                if (resources.IsResourceIdentifier(value)) {
                    integer = resources.GetIntegerByIdentifier(value);
                } else {
                    integer = int.Parse(value);
                }
            } else {
                integer = Convert.ToInt32((long) reader.Value);
            }

            return new IntegerValue(integer);
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(IntegerValue);
        }
    }
}
