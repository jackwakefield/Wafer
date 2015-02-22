using System;
using System.IO;
using Wafer.UI;
using Wafer.Utils.Config;
using Wafer.Utils.Logging;
using Wafer.Utils.Logging.Listeners;
using Wafer.Utils.Resources;
using WindowsForms = System.Windows.Forms;

namespace Wafer {
    public class Application : IApplication {
        private const string Tag = "Parchment.Application";
        private const string ConfigName = "config.json";

        private readonly IHostWindow hostWindow;
        private readonly ILogService log;
        private readonly IConfigService config;
        private readonly IResourceService resources;

        public Application(IHostWindow hostWindow, ILogService log, IConfigService config, IResourceService resources) {
            this.hostWindow = hostWindow;
            this.log = log;
            this.config = config;
            this.resources = resources;
        }

        public void Run() {
#if DEBUG
            log.AddListener(new ConsoleLogListener());
#endif

            log.Info(Tag, "Running application");

            log.Info(Tag, "Loading config from '{0}'", ConfigName);
            config.Load(ConfigName);

            log.Info(Tag, "Loading value resources");
            resources.LoadValues();

            var statusBar = resources.InflateView("status_bar");
            hostWindow.AddChild(statusBar);
            hostWindow.Run();
        }
    }
}
