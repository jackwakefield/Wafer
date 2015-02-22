using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Castle.Core.Internal;
using Wafer.UI.Direct2D.Factories;
using Wafer.UI.Views;
using SharpDX;
using Direct2D1 = SharpDX.Direct2D1;
using DirectWrite = SharpDX.DirectWrite;
using Direct3D = SharpDX.Direct3D10;
using DXGI = SharpDX.DXGI;
using SharpDX.Windows;

namespace Wafer.UI.Direct2D
{
    public class HostWindow : Form, IHostWindow, IRenderTargetProvider, IDirectWriteFactoryProvider, IView, IContext {
        public new IView Parent {
            get { return null; }
            set {
                throw new NotImplementedException();
            }
        }

        public int ActualWidth {
            get { return Width; }
        }

        public int ActualHeight {
            get { return Height; }
        }

        public Core.Point ActualPosition {
            get {
                return new Core.Point(Location.X, Location.Y);
            }
        }
        public Direct2D1.RenderTarget RenderTarget {
            get { return renderTarget; }
        }

        public DirectWrite.Factory DirectWriteFactory {
            get { return directWriteFactory; }
        }

        public ITextRenderer TextRenderer { get; set; }

        public IShapeRenderer ShapeRenderer { get; set; }

        public IColourFactory ColourFactory { get; set; }

        public ITextFormatFactory TextFormatFactory { get; set; }

        public ITextLayoutFactory TextLayoutFactory { get; set; }

        private IList<IView> children;
        private bool isResizing;

        private Direct3D.Device1 device;
        private DXGI.SwapChain swapChain;
        private Direct3D.Texture2D backBuffer;
        private Direct3D.RenderTargetView backBufferView;
        private DirectWrite.Factory directWriteFactory;
        private Direct2D1.RenderTarget renderTarget;
        private Direct2D1.Factory factory;

        public HostWindow() {
            children = new List<IView>();
        }

        public void Run() {
            InitialiseForm();
            InitialiseComponents();
            Show();

            RenderLoop.Run(this, () => {
                if (isResizing || device == null) {
                    return;
                }

                BeginDraw();
                Draw();
                EndDraw();
            });
        }

        protected void InitialiseForm() {
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            Resize += (sender, args) => OnResize();
        }

        protected void InitialiseComponents() {
            SharpDX.Configuration.EnableObjectTracking = true;

            var description = new DXGI.SwapChainDescription {
                BufferCount = 1,
                ModeDescription =
                    new DXGI.ModeDescription(Width, Height, new DXGI.Rational(60, 1), DXGI.Format.B8G8R8A8_UNorm),
                IsWindowed = true,
                OutputHandle = Handle,
                SampleDescription = new DXGI.SampleDescription(1, 0),
                SwapEffect = DXGI.SwapEffect.Discard,
                Usage = DXGI.Usage.RenderTargetOutput
            };

            Direct3D.Device1.CreateWithSwapChain(Direct3D.DriverType.Hardware,
                Direct3D.DeviceCreationFlags.BgraSupport, description, Direct3D.FeatureLevel.Level_10_0,
                out device, out swapChain);

            var dxgiFactory = swapChain.GetParent<DXGI.Factory>();
            dxgiFactory.MakeWindowAssociation(Handle, DXGI.WindowAssociationFlags.IgnoreAll);

            directWriteFactory = new DirectWrite.Factory();

            CreateSizeDependentComponents();
            children.ForEach(c => c.SetContext(this));
        }

        protected void CreateSizeDependentComponents() {
            backBuffer = Direct3D.Resource.FromSwapChain<Direct3D.Texture2D>(swapChain, 0);
            backBufferView = new Direct3D.RenderTargetView(device, backBuffer);

            factory = new Direct2D1.Factory();

            using (var surface = backBuffer.QueryInterface<DXGI.Surface>()) {
                renderTarget = new Direct2D1.RenderTarget(factory, surface,
                    new Direct2D1.RenderTargetProperties(new Direct2D1.PixelFormat(DXGI.Format.Unknown, Direct2D1.AlphaMode.Premultiplied)));
            }

            renderTarget.AntialiasMode = Direct2D1.AntialiasMode.PerPrimitive;
            renderTarget.TextAntialiasMode = Direct2D1.TextAntialiasMode.Cleartype;
        }

        public void AddChild(IView view) {
            if (!children.Contains(view)) {
                view.Parent = this;
                view.SetContext(this);
                children.Add(view);
            }
        }

        public void RemoveChild(IView view) {
            if (children.Contains(view)) {
                view.Parent = null;
                view.SetContext(null);
                children.Remove(view);
            }
        }

        public void BeginDraw() {
            device.Rasterizer.SetViewports(new Viewport(0, 0, Width, Height));
            device.OutputMerger.SetTargets(backBufferView);
            renderTarget.BeginDraw();
        }

        public void Draw() {
            renderTarget.Clear(Color.Black);
            children.ForEach(c => c.Draw());
        }

        public void EndDraw() {
            renderTarget.EndDraw();
            swapChain.Present(0, DXGI.PresentFlags.None);
        }

        public void SetContext(IContext context) {
            throw new NotImplementedException();
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            OnResize();
        }

        public void OnResize() {
            TextFormatFactory.DisposeItems();
            TextLayoutFactory.DisposeItems();
            ColourFactory.DisposeItems();

            Utilities.Dispose(ref backBuffer);
            Utilities.Dispose(ref backBufferView);
            Utilities.Dispose(ref renderTarget);

            if (swapChain != null) {
                swapChain.ResizeBuffers(1, Width, Height, DXGI.Format.B8G8R8A8_UNorm, DXGI.SwapChainFlags.None);
                CreateSizeDependentComponents();
            }

            children.ForEach(c => c.OnResize());
        }

        protected override void OnResizeBegin(EventArgs e) {
            isResizing = true;
            base.OnResizeBegin(e);
        }

        protected override void OnResizeEnd(EventArgs e) {
            base.OnResizeEnd(e);
            isResizing = false;
        }
    }
}
