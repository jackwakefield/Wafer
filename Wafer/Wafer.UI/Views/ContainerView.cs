using Castle.Core.Internal;
using Wafer.Core.Collections;
using System.Collections.Generic;

namespace Wafer.UI.Views {
    [ViewName("ContainerView")]
    public class ContainerView : View, IContainer {
        public IList<IView> Children {
            get { return children; }
        }

        private readonly ObservableList<IView> children; 

        public ContainerView() {
            children = new ObservableList<IView>();
            children.ItemAdded += args => OnItemAdded(args.Item);
            children.ItemRemoved += args => OnItemRemoved(args.Item);
        }

        public override void Draw() {
            base.Draw();

            Children.ForEach(c => c.Draw());
        }

        public override void OnResize() {
            base.OnResize();

            Children.ForEach(c => c.OnResize());
        }

        public override void SetContext(IContext context) {
            base.SetContext(context);

            Children.ForEach(c => c.SetContext(context));
        }

        protected void OnItemAdded(IView item) {
            item.Parent = this;
            
            if (context != null) {
                item.SetContext(context);
            }
        }

        protected void OnItemRemoved(IView item) {
            item.Parent = null;
            item.SetContext(null);
        }
    }
}
