using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
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
        /// Ensure the relevant registry keys exist to have a shell menu as specified for the given class.
        /// Think "install".
        /// </summary>
        /// <param name="cls">The class for which the menu should be created.</param>
        /// <param name="machineWide">Use HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE? Latter requires elevation.</param>
        public static void EnsureCreated(Model.Class cls, bool machineWide)
        {
            EnsureCreated(new[] { cls }, machineWide);
        }

        /// <summary>
        /// Ensure the relevant registry keys for a shell menu as specified do not exist for the given class.
        /// They might have been created before by <see cref="EnsureCreated(Model.Class, bool)"/>, in which
        /// case a call to this function would delete them. Think "uninstall".
        /// </summary>
        /// <param name="cls">The class for which the menu should not exist/be removed.</param>
        /// <param name="machineWide">Use HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE? Latter requires elevation.</param>
        public static void EnsureDeleted(Model.Class cls, bool machineWide)
        {
            EnsureDeleted(new[] { cls }, machineWide);
        }

        /// <summary>
        /// Ensure the relevant registry keys exist to have a shell menu as specified for the given classes.
        /// Think "install".
        /// </summary>
        /// <param name="classes">The classes for which the menus should be created.</param>
        /// <param name="machineWide">Use HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE? Latter requires elevation.</param>
        public static void EnsureCreated(IReadOnlyCollection<Model.Class> classes, bool machineWide)
        {
            using var k = OpenClasses(machineWide);
            var s = new RegistryClassesStore(k);
            s.EnsureCreated(classes);
        }

        /// <summary>
        /// Ensure the relevant registry keys for shell menus as specified do not exist for the given classes.
        /// They might have been created before by <see cref="EnsureCreated(IReadOnlyCollection{Model.Class}, bool)"/>,
        /// in which case a call to this function would delete them. Think "uninstall".
        /// </summary>
        /// <param name="classes">The classes for which the menus not exist/be removed.</param>
        /// <param name="machineWide">Use HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE? Latter requires elevation.</param>
        public static void EnsureDeleted(IReadOnlyCollection<Model.Class> classes, bool machineWide)
        {
            using var k = OpenClasses(machineWide);
            var s = new RegistryClassesStore(k);
            s.EnsureDeleted(classes);
        }

        private static RegistryKey OpenClasses(bool machineWide)
        {
            var root = machineWide ? Registry.LocalMachine : Registry.CurrentUser;
            return root.OpenSubKey(@"SOFTWARE\Classes")
                ?? throw new InvalidOperationException("Classes registry key could not be opened.");
        }
    }
}
