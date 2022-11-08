using System;
using System.Collections.Generic;
using System.Text;
using ShellStaticContextMenuManager.Model;

namespace ShellStaticContextMenuManager.Persistance
{
    /// <summary>
    /// Manages persistence of <see cref="Verb"/>s and <see cref="VerbCollection"/>s for <see cref="Class"/>es.
    /// </summary>
    internal interface IClassesStore
    {
        /// <summary>
        /// Ensures the given <see cref="Class"/>es exists in this store with their <see cref="Verb"/>s and <see cref="VerbCollection"/>s.
        /// </summary>
        /// <param name="classes">Collection of classes for which <see cref="Verb"/>s and <see cref="VerbCollection"/>s should exist.</param>
        public void EnsureCreated(IReadOnlyCollection<Class> classes);

        /// <summary>
        /// Ensures the <see cref="Verb"/>s and <see cref="VerbCollection"/>s of the given <see cref="Class"/>es don't exist in this store.
        /// </summary>
        /// <param name="classes">Collection of classes for which <see cref="Verb"/>s and <see cref="VerbCollection"/>s should not exist.</param>
        public void EnsureDeleted(IReadOnlyCollection<Class> classes);
    }
}
