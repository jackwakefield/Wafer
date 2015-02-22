/*using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Wafer.UI.Views;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using Direct2D = SharpDX.Direct2D1;
using DirectWrite = SharpDX.DirectWrite;
using Device1 = SharpDX.Direct3D10.Device1;
using DriverType = SharpDX.Direct3D10.DriverType;
using Factory = SharpDX.DXGI.Factory;
using FeatureLevel = SharpDX.Direct3D10.FeatureLevel;
using WindowsForms = System.Windows.Forms;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Resource = SharpDX.Direct3D10.Resource;

namespace Wafer.UI {
    public class MainWindow : IView, IContext {
        public int ActualWidth {
            get { return Width; }
        }

        public int ActualHeight {
            get { return Height; }
        }

        public int Width {
            get {
                if (form != null) {
                    return form.Width;
                }

                return 0;
            }
        }

        public int Height {
            get {
                if (form != null) {
                    return form.Height;
                }

                return 0;
            }
        }

        public IntPtr DisplayHandle {
            get {
                if (form != null) {
                    return form.Handle;
                }

                return IntPtr.Zero;
            }
        }
        public IView Parent {
            get { return null; }
            set {
                throw new NotImplementedException();
            }
        }

        public RenderTarget RenderTarget {
            get { return renderTarget2D; }
        }

        public DirectWrite.Factory DirectWriteFactory {
            get { return directWriteFactory; }
        }

        public IColourFactory ColourFactory { get; private set; }

        public ITextFormatFactory TextFormatFactory { get; private set; }

        public ITextLayoutFactory TextLayoutFactory { get; private set; }

        private readonly RenderForm form;
        private readonly IList<View> children;

        private Device1 device;
        private SwapChain swapChain;
        private Texture2D backBuffer;   
        private RenderTargetView backBufferView;
        private DirectWrite.Factory directWriteFactory;
        private RenderTarget renderTarget2D;
        private Direct2D.Factory factory2D;

        public MainWindow(IColourFactory colourFactory, ITextFormatFactory textFormatFactory, ITextLayoutFactory textLayoutFactory) {
            ColourFactory = colourFactory;
            TextFormatFactory = textFormatFactory;
            TextLayoutFactory = textLayoutFactory;
            BeginDraw
            children = new List<View>();

            form = new RenderForm {
                WindowState = WindowsForms.FormWindowState.Maximized,
                FormBorderStyle = WindowsForms.FormBorderStyle.None
            };

            form.Resize += (sender, args) => Resized();

            Initialise();
        }

        public void AddChild(View view) {
            if (!children.Contains(view)) {
                view.Parent = this;

                if (device != null) {
                    view.SetContext(this);
                }

                children.Add(view);
            }
        }

        public void RemoveChild(View view) {
            if (children.Contains(view)) {
                view.Parent = null;
                view.SetContext(null);
                children.Remove(view);
            }
        }

        public void Run() {
            RenderLoop.Run(form, () => {
                if (device == null) {
                    return;
                }

                BeginDraw();
                Draw();
                EndDraw();
            });
        }

        protected void Initialise() {
            var description = new SwapChainDescription {
                BufferCount = 1,
                ModeDescription =
                    new ModeDescription(Width, Height, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                IsWindowed = true,
                OutputHandle = DisplayHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            Device1.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, description, FeatureLevel.Level_10_0, out device, out swapChain);

            var factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(DisplayHandle, WindowAssociationFlags.IgnoreAll);

            directWriteFactory = new DirectWrite.Factory();
            TextFormatFactory.DirectWriteFactory = directWriteFactory;
            TextLayoutFactory.DirectWriteFactory = directWriteFactory;

            CreateSizeDependentComponents();
            children.ForEach(c => c.SetContext(this));
        }

        protected void CreateSizeDependentComponents() {
            backBuffer = Resource.FromSwapChain<Texture2D>(swapChain, 0);
            backBufferView = new RenderTargetView(device, backBuffer);

            factory2D = new Direct2D.Factory();

            using (var surface = backBuffer.QueryInterface<Surface>()) {
                renderTarget2D = new RenderTarget(factory2D, surface, new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
            }

            renderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;
            renderTarget2D.TextAntialiasMode = TextAntialiasMode.Cleartype;
            ColourFactory.RenderTarget = renderTarget2D;
        }

        protected void Resized() {
            ColourFactory.DisposeItems();
            TextFormatFactory.DisposeItems();
            TextLayoutFactory.DisposeItems();

            if (backBuffer != null && !backBuffer.IsDisposed) {
                backBuffer.Dispose();
                backBufferView.Dispose();
            }

            if (renderTarget2D != null && !renderTarget2D.IsDisposed) {
                renderTarget2D.Dispose();
            }

            if (swapChain != null) {
                swapChain.ResizeBuffers(1, Width, Height, Format.B8G8R8A8_UNorm, SwapChainFlags.None);
                CreateSizeDependentComponents();
            }

            children.ForEach(c => c.ParentResized());
        }

        protected void BeginDraw() {
            device.Rasterizer.SetViewports(new Viewport(0, 0, Width, Height));
            device.OutputMerger.SetTargets(backBufferView);
            renderTarget2D.BeginDraw();
        }

        protected void Draw() {
            children.ForEach(c => c.Draw());
        }

        protected void EndDraw() {
            renderTarget2D.EndDraw();
            swapChain.Present(0, PresentFlags.None);
        }

        public void SetContext(IContext context) {
            throw new NotImplementedException();
        }
    }
}
*/