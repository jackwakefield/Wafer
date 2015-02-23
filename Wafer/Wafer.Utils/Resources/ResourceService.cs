using Castle.Core.Internal;
using Newtonsoft.Json;
using Wafer.Core;
using Wafer.Core.Json;
using Wafer.Utils.Config;
using Wafer.Utils.Logging;
using Wafer.Utils.Resources.Exceptions;
using Wafer.Utils.Resources.Json.Converters;
using Wafer.Utils.Resources.Json.Values;
using System.Collections.Generic;
using System.IO;

namespace Wafer.Utils.Resources {
    public class ResourceService : IResourceService {
        private const string Tag = "ResourceService";

        private const string FileExtension = ".json";
        private const string FilePattern = "*" + FileExtension;

        private const string DimensionResourceIdentifier = "@Dimension/";
        private const string StringResourceIdentifier = "@String/";
        private const string IntegerResourceIdentifier = "@Integer/";
        private const string ColourResourceIdentifier = "@Colour/";

        private static readonly string[] ValueFiles = new string[] {
            "base_colours.json",
            "colours.json",
            "dimensions.json"
        };

        public IDictionary<string, Dimension> Dimensions { get; private set; }

        public IDictionary<string, int> Integers { get; private set; }

        public IDictionary<string, string> Strings { get; private set; }

        public IDictionary<string, Colour> Colours { get; private set; }

        private readonly ILogService log;

        private readonly IConfigService config;

        public ResourceService(ILogService log, IConfigService config) {
            this.log = log;
            this.config = config;

            Dimensions = new Dictionary<string, Dimension>();
            Integers = new Dictionary<string, int>();
            Strings = new Dictionary<string, string>();
            Colours = new Dictionary<string, Colour>();
        }

        public void LoadValues() {
            var typesBinder = new MappedTypesBinder {
                KnownTypes = {
                    { "String", typeof(StringResource) },
                    { "Integer", typeof(IntegerResource) },
                    { "Dimension", typeof(DimensionResource) },
                    { "Colour", typeof(ColourResource) }
                }
            };

            foreach (var file in ValueFiles) {
                var path = Path.Combine(config.Paths.Values, file);
                log.Verbose(Tag, "Deserialising '{0}'", path);

                var content = File.ReadAllText(path);

                var values = JsonConvert.DeserializeObject<Dictionary<string, IValueResource>>(content, new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Binder = typesBinder,
                    Converters = new JsonConverter[] {
                        new DimensionConverter(this),
                        new ColourConverter(this),
                        new StringConverter(this), 
                        new IntegerConverter(this)
                    }
                });

                values.ForEach(p => {
                    var key = p.Key;
                    var resource = p.Value;

                    if (resource is DimensionResource) {
                        if (Dimensions.ContainsKey(key)) {
                            throw new DuplicateValueNameException("Dimension", key);
                        }

                        Dimensions.Add(key, (resource as DimensionResource).Value);
                    } else if (resource is IntegerResource) {
                        if (Integers.ContainsKey(key)) {
                            throw new DuplicateValueNameException("Integer", key);
                        }

                        Integers.Add(key, (resource as IntegerResource).Value);
                    } else if (resource is StringResource) {
                        if (Strings.ContainsKey(key)) {
                            throw new DuplicateValueNameException("String", key);
                        }

                        Strings.Add(key, (resource as StringResource).Value);
                    } else if (resource is ColourResource) {
                        if (Colours.ContainsKey(key)) {
                            throw new DuplicateValueNameException("Colour", key);
                        }

                        Colours.Add(key, (resource as ColourResource).Value);
                    }
                });
            }
        }

        public bool IsResourceIdentifier(string value) {
            return value.StartsWith(DimensionResourceIdentifier)
                || value.StartsWith(StringResourceIdentifier)
                || value.StartsWith(IntegerResourceIdentifier)
                || value.StartsWith(ColourResourceIdentifier);
        }

        public Dimension GetDimensionByIdentifier(string value) {
            var key = value.Substring(DimensionResourceIdentifier.Length);

            if (Dimensions.ContainsKey(key)) {
                return Dimensions[key];
            }

            throw new UnknownResourceException("Dimension", key);
        }

        public string GetStringByIdentifier(string value) {
            var key = value.Substring(StringResourceIdentifier.Length);

            if (Strings.ContainsKey(key)) {
                return Strings[key];
            }

            throw new UnknownResourceException("String", key);
        }

        public int GetIntegerByIdentifier(string value) {
            var key = value.Substring(IntegerResourceIdentifier.Length);

            if (Integers.ContainsKey(key)) {
                return Integers[key];
            }

            throw new UnknownResourceException("Integer", key);
        }

        public Colour GetColourByIdentifier(string value) {
            var key = value.Substring(ColourResourceIdentifier.Length);

            if (Colours.ContainsKey(key)) {
                return Colours[key];
            }

            throw new UnknownResourceException("Colour", key);
        }
    }
}
