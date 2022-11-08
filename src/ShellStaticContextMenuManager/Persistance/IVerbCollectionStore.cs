using System;
using System.Collections.Generic;
using System.Text;
using ShellStaticContextMenuManager.Model;

namespace ShellStaticContextMenuManager.Persistance
{
    /// <summary>
    /// Manages persisting <see cref="VerbCollection"/>s.
    /// </summary>
    internal interface IVerbCollectionStore
    {
        /// <summary>
        /// Ensures the given <see cref="VerbCollection"/> exists in this store.
        /// </summary>
        /// <param name="collection">The <see cref="VerbCollection"/> that needs to exist.</param>
        public void EnsureCreated(VerbCollection collection);

        /// <summary>
        /// Ensures the given <see cref="VerbCollection"/> does not exist in this store.
        /// </summary>
        /// <param name="collection">The <see cref="VerbCollection"/> that may not exist.</param>
        public void EnsureDeleted(VerbCollection collection);
    }
}
