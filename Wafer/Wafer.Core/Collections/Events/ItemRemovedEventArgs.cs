using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafer.Core.Collections.Events {
    public class ItemRemovedEventArgs<T> : EventArgs {
        public T Item { get; private set; }

        public ItemRemovedEventArgs(T item) {
            Item = item;
        }
    }
}
