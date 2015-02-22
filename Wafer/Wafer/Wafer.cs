using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Wafer {
    public class Wafer {
        public static void Main(string[] args) {
            var container = BootstrapContainer();

            var application = container.Resolve<IApplication>();
            application.Run();

            container.Dispose();
        }

        public static IWindsorContainer BootstrapContainer() {
            return new WindsorContainer()
                .Install(FromAssembly.This())
                .Install(FromAssembly.Named("Wafer.Utils"))
                .Install(FromAssembly.Named("Wafer.Core"))
                .Install(FromAssembly.Named("Wafer.UI"))
                .Install(FromAssembly.Named("Wafer.UI.Direct2D"))
                .Install(FromAssembly.Named("Wafer.StatusBar"));
        }
    }
}
