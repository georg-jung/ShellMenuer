using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShellStaticContextMenuManager.Model
{
    /// <summary>
    /// Holds a collection of <see cref="Verb"/> instances and the relative path where they should be kept.
    /// Note that a collection just needs to be kept below one class and can be referenced from other classes.
    /// </summary>
    // don't inherit from List<T> https://stackoverflow.com/q/21692193/1200847
    public class VerbCollection : ICollection<Verb>
    {
        /// <summary>
        /// Gets a collection of <see cref="Verb"/> instances that are part of this collection.
        /// </summary>
        private readonly ICollection<Verb> _verbs = new List<Verb>();

        /// <summary>
        /// Initializes a new instance of the <see cref="VerbCollection"/> class.
        /// </summary>
        /// <param name="path">The value of the <see cref="Path"/> property.</param>
        public VerbCollection(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (path.StartsWith(@"\") || path.EndsWith(@"\"))
            {
                throw new ArgumentException("path should not start or end with a backslash.", nameof(path));
            }

            if (path.Contains("/"))
            {
                throw new ArgumentException("path contains a forward slash, which is invalid. Did you mean to use a backslash?", nameof(path));
            }

            Path = path;
        }

        /// <summary>
        /// Gets the path of the key where the collection should be kept. E.g. "ContextMenus\MyGreatApp".
        /// </summary>
        public string Path { get; }

        /// <inheritdoc/>
        public int Count => _verbs.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => _verbs.IsReadOnly;

        /// <inheritdoc/>
        public void Add(Verb item)
        {
            _verbs.Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _verbs.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(Verb item)
        {
            return _verbs.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(Verb[] array, int arrayIndex)
        {
            _verbs.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<Verb> GetEnumerator()
        {
            return _verbs.GetEnumerator();
        }

        /// <inheritdoc/>
        public bool Remove(Verb item)
        {
            return _verbs.Remove(item);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_verbs).GetEnumerator();
        }
    }
}
