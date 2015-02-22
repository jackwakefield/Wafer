using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafer.Core.Collections.Events;

namespace Wafer.Core.Collections {
    public class ObservableList<T> : IList<T> {
        public int Count {
            get { return shadowList.Count; }
        }

        public bool IsReadOnly {
            get { return shadowList.IsReadOnly; }
        }

        public T this[int index] {
            get { return shadowList[index]; }
            set { shadowList[index] = value; }
        }

        public delegate void ItemAddedDelegate(ItemAddedEventArgs<T> args);
        public event ItemAddedDelegate ItemAdded;

        public delegate void ItemRemovedDelegate(ItemRemovedEventArgs<T> args);
        public event ItemRemovedDelegate ItemRemoved;

        private readonly IList<T> shadowList;

        public ObservableList() {
            shadowList = new List<T>();
        }

        public IEnumerator<T> GetEnumerator() {
            return shadowList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(T item) {
            shadowList.Add(item);

            if (ItemAdded != null) {
                ItemAdded(new ItemAddedEventArgs<T>(item));
            }
        }

        public void Clear() {
            T[] items = shadowList.ToArray();
            shadowList.Clear();

            if (ItemRemoved != null) {
                for (var i = 0; i < items.Length; i++) {
                    ItemRemoved(new ItemRemovedEventArgs<T>(items[i]));
                }
            }
        }

        public bool Contains(T item) {
            return shadowList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            shadowList.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item) {
            var result = shadowList.Remove(item);
            
            if (ItemRemoved != null) {
                ItemRemoved(new ItemRemovedEventArgs<T>(item));
            }

            return result;
        }

        public int IndexOf(T item) {
            return shadowList.IndexOf(item);
        }

        public void Insert(int index, T item) {
            shadowList.Insert(index, item);

            if (ItemAdded != null) {
                ItemAdded(new ItemAddedEventArgs<T>(item));
            }
        }

        public void RemoveAt(int index) {
            var item = shadowList[index];
            shadowList.RemoveAt(index);

            if (ItemRemoved != null) {
                ItemRemoved(new ItemRemovedEventArgs<T>(item));
            }
        }
    }
}
