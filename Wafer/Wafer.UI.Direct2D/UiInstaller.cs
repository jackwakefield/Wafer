using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Wafer.UI.Direct2D.Factories;

namespace Wafer.UI.Direct2D {
    public class UiInstaller : IWindsorInstaller {
        public void Install(IWindsorContainer container, IConfigurationStore store) {
            container.Register(
                Component.For<IHostWindow, IRenderTargetProvider, IDirectWriteFactoryProvider,
                    IDeviceProvider, IImagingFactoryProvider>().ImplementedBy<HostWindow>().LifestyleSingleton(),
                Component.For<ITextRenderer>().ImplementedBy<TextRenderer>().LifestyleSingleton(),
                Component.For<IShapeRenderer>().ImplementedBy<ShapeRenderer>().LifestyleSingleton(),
                Component.For<IImageRenderer>().ImplementedBy<ImageRenderer>().LifestyleSingleton(),
                Component.For<IImageLoader>().ImplementedBy<ImageLoader>().LifestyleSingleton(),
                Component.For<IColourFactory>().ImplementedBy<ColourFactory>().LifestyleSingleton(),
                Component.For<ITextFormatFactory>().ImplementedBy<TextFormatFactory>().LifestyleSingleton(),
                Component.For<ITextLayoutFactory>().ImplementedBy<TextLayoutFactory>().LifestyleSingleton()
            );
        }
    }
}
