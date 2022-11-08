using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using ShellMenuer.Model;

namespace ShellMenuer.Persistance
{
    /// <summary>
    /// Manages persistence of <see cref="Verb"/>s and <see cref="VerbCollection"/>s for <see cref="Class"/>es in Windows' Registry.
    /// </summary>
    internal class RegistryClassesStore : IClassesStore
    {
        private readonly RegistryKey _classesKey;

        public RegistryClassesStore(RegistryKey classesKey)
        {
            _classesKey = classesKey;
        }

        /// <inheritdoc/>
        public void EnsureCreated(IReadOnlyCollection<Class> classes)
        {
            EnsureClassesValid(classes);

            foreach (var cls in classes)
            {
                using var clsKey = _classesKey.CreateSubKey(cls.Identifier);

                foreach (var vrbCols in cls.VerbCollections)
                {
                    var colStore = new RegistryVerbCollectionStore(clsKey, VerbStoreFactory(cls));
                    colStore.EnsureCreated(vrbCols);
                }

                var vrbStore = new RegistryVerbStore(clsKey, cls.Identifier);
                foreach (var verb in cls.Verbs)
                {
                    vrbStore.EnsureCreated(verb);
                }
            }
        }

        /// <inheritdoc/>
        public void EnsureDeleted(IReadOnlyCollection<Class> classes)
        {
            EnsureClassesValid(classes);

            foreach (var cls in classes)
            {
                using var clsKey = _classesKey.CreateSubKey(cls.Identifier);

                foreach (var vrbCols in cls.VerbCollections)
                {
                    var colStore = new RegistryVerbCollectionStore(clsKey, VerbStoreFactory(cls));
                    colStore.EnsureDeleted(vrbCols);
                }

                var vrbStore = new RegistryVerbStore(clsKey, cls.Identifier);
                foreach (var verb in cls.Verbs)
                {
                    vrbStore.EnsureDeleted(verb);
                }
            }
        }

        private static Func<RegistryKey, IVerbStore> VerbStoreFactory(Class cls) => (RegistryKey key) => new RegistryVerbStore(key, cls.Identifier);

        private void EnsureClassesValid(IReadOnlyCollection<Class> classes)
        {
            void EnsurePathsUnique(Class cl)
            {
                var paths = cl.VerbCollections.Select(vc => vc.Path);
                if (cl.VerbCollections.Distinct().Count() != paths.Distinct(StringComparer.OrdinalIgnoreCase).Count())
                {
                    throw new ArgumentException($"Class {cl.Identifier} is modelled as parent of at least one VerbCollection with a non-unique Path.");
                }
            }

            foreach (var cl in classes)
            {
                EnsurePathsUnique(cl);
            }
        }
    }
}
