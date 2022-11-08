using System;
using System.Collections.Generic;
using System.Text;

namespace ShellMenuer
{
    /// <summary>
    /// Nicer wrappers around native methods.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Notifies the Windows Explorer Shell that the file type associations have changed.
        /// </summary>
        public static unsafe void ShChangeNotify_AssocChanged()
        {
            Windows.Win32.PInvoke.SHChangeNotify(Windows.Win32.UI.Shell.SHCNE_ID.SHCNE_ASSOCCHANGED, Windows.Win32.UI.Shell.SHCNF_FLAGS.SHCNF_IDLIST);
        }
    }
}
