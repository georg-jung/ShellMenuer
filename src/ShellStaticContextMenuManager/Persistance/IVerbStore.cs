using System;
using System.Collections.Generic;
using System.Text;
using ShellStaticContextMenuManager.Model;

namespace ShellStaticContextMenuManager.Persistance
{
    /// <summary>
    /// Manages persisting <see cref="Verb"/>s.
    /// </summary>
    internal interface IVerbStore
    {
        /// <summary>
        /// Ensures the given <see cref="Verb"/> exists in this store.
        /// </summary>
        /// <param name="verb">The <see cref="Verb"/> that needs to exist.</param>
        public void EnsureCreated(Verb verb);

        /// <summary>
        /// Ensures the given <see cref="Verb"/> does not exist in this store.
        /// </summary>
        /// <param name="verb">The <see cref="Verb"/> that may not exist.</param>
        public void EnsureDeleted(Verb verb);
    }
}
