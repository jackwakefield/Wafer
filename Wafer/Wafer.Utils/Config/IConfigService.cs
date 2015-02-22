using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.Utils.Config {
    public interface IConfigService {
        ConfigPaths Paths { get; }

        void Load(string path);
    }
}
