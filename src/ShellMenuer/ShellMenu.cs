using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using ShellMenuer.Model;
using ShellMenuer.Persistance;

namespace ShellMenuer
{
    /// <summary>
    /// Enables you to create or remove (cascading) Windows Explorer Context Menu entries. See also
    /// https://learn.microsoft.com/en-us/windows/win32/shell/context-menu-handlers#creating-cascading-menus-with-the-extendedsubcommandskey-registry-entry.
    /// </summary>
    public static class ShellMenu
    {
        /// <summary>
        /// Ensure the relevant registry keys exist to have a shell menu as specified.
        /// Think "install".
        /// </summary>
        /// <param name="scope">Specifies which context menu should change.</param>
        /// <param name="verb">The entry to add.</param>
        /// <param name="machineWide">Use HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE? Latter requires elevation.</param>
        public static void EnsureCreated(ShellMenuScope scope, Verb verb, bool machineWide = false)
            => EnsureCreated(scope, new[] { verb }, machineWide);

        /// <summary>
        /// Ensure the relevant registry keys exist to have a shell menu as specified.
        /// Think "install".
        /// </summary>
        /// <param name="scope">Specifies which context menu should change.</param>
        /// <param name="verbs">The entries to add.</param>
        /// <param name="machineWide">Use HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE? Latter requires elevation.</param>
        public static void EnsureCreated(ShellMenuScope scope, IEnumerable<Verb> verbs, bool machineWide = false)
        {
            var relPath = GetRelativeClassKeyPath(scope);
            using var k = OpenClassKey(relPath, machineWide);
            var s = new RegistryVerbStore(k, relPath);

            foreach (var v in verbs)
            {
                s.EnsureCreated(v);
            }

            NativeMethods.ShChangeNotify_AssocChanged();
        }

        /// <summary>
        /// Ensure the relevant registry keys to have a shell menu as specified do not exist.
        /// They might have been created before by <see cref="EnsureCreated(ShellMenuScope, Verb, bool)"/>,
        /// in which case a call to this function would delete them. Think "uninstall".
        /// </summary>
        /// <param name="scope">Specifies which context menu should change.</param>
        /// <param name="verb">The entry to remove.</param>
        /// <param name="machineWide">Use HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE? Latter requires elevation.</param>
        public static void EnsureDeleted(ShellMenuScope scope, Verb verb, bool machineWide = false)
            => EnsureDeleted(scope, new[] { verb }, machineWide);

        /// <summary>
        /// Ensure the relevant registry keys to have a shell menu as specified do not exist.
        /// They might have been created before by <see cref="EnsureCreated(ShellMenuScope, IEnumerable{Verb}, bool)"/>,
        /// in which case a call to this function would delete them. Think "uninstall".
        /// </summary>
        /// <param name="scope">Specifies which context menu should change.</param>
        /// <param name="verbs">The entries to remove.</param>
        /// <param name="machineWide">Use HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE? Latter requires elevation.</param>
        public static void EnsureDeleted(ShellMenuScope scope, IEnumerable<Verb> verbs, bool machineWide = false)
        {
            var relPath = GetRelativeClassKeyPath(scope);
            using var k = OpenClassKey(relPath, machineWide);
            var s = new RegistryVerbStore(k, relPath);

            foreach (var v in verbs)
            {
                s.EnsureDeleted(v);
            }

            NativeMethods.ShChangeNotify_AssocChanged();
        }

        private static string GetRelativeClassKeyPath(ShellMenuScope scope) => scope switch
        {
            ShellMenuScope.FileItems => "*",
            ShellMenuScope.DirectoryItems => "Directory",
            ShellMenuScope.DirectoryBackground => "Directory\\Background",
            _ => throw new NotImplementedException(),
        };

        /// <param name="relativeKey">E.g. "*", "Directory", "Directory\Background".</param>
        /// <param name="machineWide">Use HKLM or HKCU.</param>
        private static RegistryKey OpenClassKey(string relativeKey, bool machineWide)
        {
            var root = machineWide ? Registry.LocalMachine : Registry.CurrentUser;
            return root.OpenSubKey(@$"SOFTWARE\Classes\{relativeKey}", true)
                ?? throw new InvalidOperationException("Classes registry key could not be opened.");
        }
    }
}
