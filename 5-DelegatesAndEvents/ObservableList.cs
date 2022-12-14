namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private IList<TItem> _list = new List<TItem>();

        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => _list.Count;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => _list.IsReadOnly;

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get => _list[index];
            set
            {
                ElementChanged.Invoke(this, value, _list[index], index);
                _list[index] = value;
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator() => _list.GetEnumerator();

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            _list.Add(item);
            ElementInserted?.Invoke(this, item, IndexOf(item));
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear() => _list.Clear();

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item) => _list.Contains(item);

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            int index = IndexOf(item);
            if(!_list.Remove(item)) return false;
            ElementRemoved.Invoke(this, item, index);
            return true;
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item) => _list.IndexOf(item);

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            _list.Insert(index, item);
            ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            var removed = _list[index];
            _list.RemoveAt(index);
            ElementRemoved.Invoke(this, removed, index);
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj) => obj is ObservableList<TItem> obList && Equals(obList);

        /// <inheritdoc cref="object.Equals(object?)" />
        public bool Equals(ObservableList<TItem> obList)
        {
            if (Count != obList.Count) return false;
            for(int i = 0; i < Count; i++)
                if(!this[i].Equals(obList[i])) return false;
            return true;
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode() => HashCode.Combine(_list);

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            string s = "[";
            foreach(var el in _list)
                s+= el + ",";
            s = s.Substring(0,s.Length - 1) + "]";
            return s;
        }
    }
}
