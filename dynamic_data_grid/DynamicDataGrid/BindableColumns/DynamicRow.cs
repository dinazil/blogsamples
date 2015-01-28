using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System.Collections;

namespace DynamicDataGrid.BindableColumns
{
    /// <summary>
    /// Represents a row in a DataGrid with dynamic column binding.
    /// </summary>
    /// <typeparam name="THeader">The type of header of the row.</typeparam>
    /// <typeparam name="TProperties">The type of properties in the row.</typeparam>
    public class DynamicRow<THeader, TProperties> : ObservableObject, IList<TProperties>
    {
        private THeader _title;
        private readonly ObservableCollection<TProperties> _properties = new ObservableCollection<TProperties>();

        /// <summary>
        /// Gets or sets the title of the row
        /// </summary>
        public THeader Title
        {
            get { return _title; }
            set { Set(() => Title, ref _title, value); }
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="DynamicRow{THeader,TProperties}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="DynamicRow{THeader,TProperties}"/>.</param>
        /// <returns>The index of <paramref name="item" /> if found in the list; otherwise, -1.</returns>
        public int IndexOf(TProperties item)
        {
            return _properties.IndexOf(item);
        }

        /// <summary>Inserts an item to the <see cref="DynamicRow{THeader,TProperties}"/> at the specified index.</summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="DynamicRow{THeader,TProperties}"/>.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> is not a valid index in the <see cref="DynamicRow{THeader,TProperties}"/>.</exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="DynamicRow{THeader,TProperties}"/> is read-only.</exception>
        public void Insert(int index, TProperties item)
        {
            _properties.Insert(index, item);
        }

        /// <summary>Removes the <see cref="DynamicRow{THeader,TProperties}"/> item at the specified index.</summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> is not a valid index in the <see cref="DynamicRow{THeader,TProperties}"/>.</exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="DynamicRow{THeader,TProperties}"/> is read-only.</exception>
        public void RemoveAt(int index)
        {
            _properties.RemoveAt(index);
        }

        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <returns>The element at the specified index.</returns>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> is not a valid index in the <see cref="DynamicRow{THeader,TProperties}"/>.</exception>
        /// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="DynamicRow{THeader,TProperties}"/> is read-only.</exception>
        public TProperties this[int index]
        {
            get { return _properties[index]; }
            set { _properties[index] = value; }
        }

        /// <summary>Adds an item to the <see cref="DynamicRow{THeader,TProperties}"/>.</summary>
        /// <param name="item">The object to add to the <see cref="DynamicRow{THeader,TProperties}"/>.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="DynamicRow{THeader,TProperties}"/> is read-only.</exception>
        public void Add(TProperties item)
        {
            _properties.Add(item);
        }

        /// <summary>Removes all items from the <see cref="DynamicRow{THeader,TProperties}"/>.</summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="DynamicRow{THeader,TProperties}"/> is read-only. </exception>
        public void Clear()
        {
            _properties.Clear();
        }

        /// <summary>Determines whether the <see cref="DynamicRow{THeader,TProperties}"/> contains a specific value.</summary>
        /// <returns>true if <paramref name="item" /> is found in the <see cref="DynamicRow{THeader,TProperties}"/>; otherwise, false.</returns>
        /// <param name="item">The object to locate in the <see cref="DynamicRow{THeader,TProperties}"/>.</param>
        public bool Contains(TProperties item)
        {
            return _properties.Contains(item);
        }

        /// <summary>Copies the elements of the <see cref="DynamicRow{THeader,TProperties}"/> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="DynamicRow{THeader,TProperties}"/>. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="arrayIndex" /> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="DynamicRow{THeader,TProperties}"/> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
        public void CopyTo(TProperties[] array, int arrayIndex)
        {
            _properties.CopyTo(array, arrayIndex);
        }

        /// <summary>Gets the number of elements contained in the <see cref="DynamicRow{THeader,TProperties}"/>.</summary>
        /// <returns>The number of elements contained in the <see cref="DynamicRow{THeader,TProperties}"/>.</returns>
        public int Count
        {
            get { return _properties.Count; }
        }

        /// <summary>Gets a value indicating whether the <see cref="DynamicRow{THeader,TProperties}"/> is read-only.</summary>
        /// <returns>false</returns>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>Removes the first occurrence of a specific object from the <see cref="DynamicRow{THeader,TProperties}"/>.</summary>
        /// <returns>true if <paramref name="item" /> was successfully removed from the <see cref="DynamicRow{THeader,TProperties}"/>; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="DynamicRow{THeader,TProperties}"/>.</returns>
        /// <param name="item">The object to remove from the <see cref="DynamicRow{THeader,TProperties}"/>.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="DynamicRow{THeader,TProperties}"/> is read-only.</exception>
        public bool Remove(TProperties item)
        {
            return _properties.Remove(item);
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerator{T}" /> that can be used to iterate through the collection.</returns>
        public IEnumerator<TProperties> GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_properties).GetEnumerator();
        }
    }
}
