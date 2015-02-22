using System.IO;
using Newtonsoft.Json;

namespace Wafer.Utils.Config {
    public class ConfigService : IConfigService {
        public ConfigPaths Paths {
            get { return config.Paths; }
        }

        private Config config;

        public void Load(string path) {
            var text = File.ReadAllText(path);
            config = JsonConvert.DeserializeObject<Config>(text);
        }
    }
}
