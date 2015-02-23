using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Wafer.UI {
    public class UiInstaller : IWindsorInstaller {
        public void Install(IWindsorContainer container, IConfigurationStore store) {
            container.Register(
                Component.For<ILayoutInflater>().ImplementedBy<LayoutInflater>().LifestyleSingleton()
            );
        }
    }
}
