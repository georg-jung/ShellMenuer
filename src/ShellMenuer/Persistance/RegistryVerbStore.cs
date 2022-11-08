using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using ShellMenuer.Model;

namespace ShellMenuer.Persistance
{
    internal class RegistryVerbStore : IVerbStore
    {
        private const string ExtendedSubCommandsKey = "ExtendedSubCommandsKey";
        private const string MUIVerb = "MUIVerb";
        private const string Extended = "Extended";
        private const string Icon = "Icon";
        private readonly RegistryKey _parentKey;
        private readonly string _pathRelativeToClassesKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryVerbStore"/> class.
        /// </summary>
        /// <param name="parentKey">The key below which the verbs are stored. This needs to be the key above
        /// the "shell" key. Needs to be writable. E.g. "HKEY_CURRENT_USER\SOFTWARE\Classes\*". Can be e.g.
        /// "HKEY_CURRENT_USER\SOFTWARE\Classes\*\ContextMenus\MyGreatApp" either if this is the verb store
        /// for a VerbCollection to be used as cascading menu.</param>
        /// <param name="pathRelativeToClassesKey">The path of the given parentKey below "Classes".
        /// E.g. this should be "*" for a <paramref name="parentKey"/>
        /// "HKEY_CURRENT_USER\SOFTWARE\Classes\*" or "*\shell\MyGreatApp" for
        /// "HKEY_CURRENT_USER\SOFTWARE\Classes\*\shell\MyGreatApp".</param>
        public RegistryVerbStore(RegistryKey parentKey, string pathRelativeToClassesKey)
        {
            _parentKey = parentKey;
            _pathRelativeToClassesKey = pathRelativeToClassesKey;
        }

        /// <inheritdoc/>
        public void EnsureCreated(Verb verb)
        {
            var relativeVerbPath = @$"shell\{verb.Name}";
            using var verbKey = _parentKey.CreateSubKey(relativeVerbPath);
            verbKey.SetValue(MUIVerb, verb.MuiVerb, RegistryValueKind.String);
            if (verb.Extended)
            {
                verbKey.SetValue(Extended, string.Empty, RegistryValueKind.String);
            }
            else
            {
                verbKey.DeleteValue(Extended, false);
            }

            if (verb.Icon != null)
            {
                verbKey.SetValue(Icon, verb.Icon, RegistryValueKind.String);
            }
            else
            {
                verbKey.DeleteValue(Icon, false);
            }

            switch (verb)
            {
                case NodeVerb node:
                {
                    using var cmdKey = verbKey.CreateSubKey("command");
                    cmdKey.SetValue(null, node.CommandText, RegistryValueKind.String);
                    break;
                }

                case CascadingVerb csc:
                {
                    // The path to the sub menu items would not need to be below the expanding item/parent.
                    // It seems to makes sense though because it is easy to correlate and is what Microsoft did with
                    // HKEY_CLASSES_ROOT\*\shell\UpdateEncryptionSettingsWork
                    var loc = @$"{_pathRelativeToClassesKey}\{relativeVerbPath}";
                    verbKey.SetValue(ExtendedSubCommandsKey, loc, RegistryValueKind.String);

                    var childStore = new RegistryVerbStore(verbKey, loc);
                    foreach (var child in csc.Children)
                    {
                        childStore.EnsureCreated(child);
                    }

                    break;
                }

                default:
                    throw new NotImplementedException();
            }
        }

        /// <inheritdoc/>
        public void EnsureDeleted(Verb verb)
        {
            _parentKey.DeleteSubKeyTree(@$"shell\{verb.Name}", false);
        }
    }
}
