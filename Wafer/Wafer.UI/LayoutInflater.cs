using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wafer.Core.Json;
using Wafer.UI.Views;
using Wafer.Utils.Config;
using Wafer.Utils.Resources;
using Wafer.Utils.Resources.Json.Converters;

namespace Wafer.UI {
    public class LayoutInflater : ILayoutInflater {
        private const string FileExtension = ".json";

        private readonly IConfigService config;
        private readonly IResourceService resources;

        public LayoutInflater(IConfigService config, IResourceService resources) {
            this.config = config;
            this.resources = resources;
        }

        public View Inflate(string name) {
            if (!name.EndsWith(FileExtension)) {
                name += FileExtension;
            }

            var path = Path.Combine(config.Paths.Layouts, name);

            var typesBinder = new MappedTypesBinder {
                KnownTypes = ViewHelper.GetViewTypes()
            };

            var content = File.ReadAllText(path);

            var view = JsonConvert.DeserializeObject<View>(content, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects,
                Binder = typesBinder,
                Converters = new JsonConverter[] {
                    new DimensionConverter(resources),
                    new ColourConverter(resources),
                    new StringConverter(resources), 
                    new IntegerConverter(resources)
                }
            });

            return view;
        }
    }
}
