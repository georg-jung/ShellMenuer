using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Win32;
using ShellStaticContextMenuManager.Model;

namespace ShellStaticContextMenuManager.Persistance
{
    internal class RegistryVerbCollectionStore : IVerbCollectionStore
    {
        private readonly RegistryKey _parentKey;
        private readonly Func<RegistryKey, IVerbStore> _verbStoreFactory;

        public RegistryVerbCollectionStore(RegistryKey parentKey, Func<RegistryKey, IVerbStore> verbStoreFactory)
        {
            _parentKey = parentKey;
            _verbStoreFactory = verbStoreFactory;
        }

        /// <inheritdoc/>
        public void EnsureCreated(VerbCollection collection)
        {
            using var collectionKey = _parentKey.CreateSubKey(collection.Path);
            var store = CreateVerbStore(collectionKey);
            foreach (var verb in collection)
            {
                store.EnsureCreated(verb);
            }
        }

        /// <inheritdoc/>
        public void EnsureDeleted(VerbCollection collection)
        {
            _parentKey.DeleteSubKeyTree(collection.Path, false);
        }

        private IVerbStore CreateVerbStore(RegistryKey parentKey) => _verbStoreFactory(parentKey);
    }
}
