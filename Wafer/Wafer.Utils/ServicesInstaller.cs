using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Wafer.Utils.Config;
using Wafer.Utils.Logging;
using Wafer.Utils.Resources;

namespace Wafer.Utils {
    public class ServicesInstaller : IWindsorInstaller {
        public void Install(IWindsorContainer container, IConfigurationStore store) {
            container.Register(
                Component.For<ILogService>().ImplementedBy<LogService>().LifestyleSingleton(),
                Component.For<IConfigService>().ImplementedBy<ConfigService>().LifestyleSingleton(),
                Component.For<IResourceService>().ImplementedBy<ResourceService>().LifestyleSingleton()
            );
        }
    }
}
