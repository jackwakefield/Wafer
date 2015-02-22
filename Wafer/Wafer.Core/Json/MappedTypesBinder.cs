using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Wafer.Core.Json {
    public class MappedTypesBinder : SerializationBinder {
        public IDictionary<string, Type> KnownTypes { get; set; }

        public MappedTypesBinder() {
            KnownTypes = new Dictionary<string, Type>();
        }

        public override Type BindToType(string assemblyName, string typeName) {
            return KnownTypes.SingleOrDefault(p => p.Key == typeName).Value;
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName) {
            assemblyName = null;
            typeName = KnownTypes.SingleOrDefault(p => p.Value == serializedType).Key;
        }
    }
}
