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
        private readonly string _className;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryVerbStore"/> class.
        /// </summary>
        /// <param name="parentKey">The key below which the verbs are stored. This needs to be the key above
        /// the "shell" key. Needs to be writable. E.g. "HKEY_CURRENT_USER\SOFTWARE\Classes\*". Can be e.g.
        /// "HKEY_CURRENT_USER\SOFTWARE\Classes\*\ContextMenus\MyGreatApp" either if this is the verb store
        /// for a VerbCollection to be used as cascading menu.</param>
        /// <param name="classIdentifier">The name/identifier of the class where the verbs will be stored.
        /// E.g. this should be "*" for <paramref name="parentKey"/> values such as
        /// "HKEY_CURRENT_USER\SOFTWARE\Classes\*" or
        /// "HKEY_CURRENT_USER\SOFTWARE\Classes\*\ContextMenus\MyGreatApp".</param>
        public RegistryVerbStore(RegistryKey parentKey, string classIdentifier)
        {
            _parentKey = parentKey;
            _className = classIdentifier;
        }

        /// <inheritdoc/>
        public void EnsureCreated(Verb verb)
        {
            using var verbKey = _parentKey.CreateSubKey(@$"shell\{verb.Name}");
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
                    var loc = @$"{_className}\{csc.Children.Path}";
                    verbKey.SetValue(ExtendedSubCommandsKey, loc, RegistryValueKind.String);
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
