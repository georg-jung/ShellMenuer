using System;
using System.Collections.Generic;
using System.Text;

namespace ShellMenuer
{
    /// <summary>
    /// On which kinds of context menus should we work?
    /// </summary>
    public enum ShellMenuScope
    {
        /// <summary>
        /// All kinds of files, no folders; use placeholder %1.
        /// </summary>
        FileItems = 1,

        /// <summary>
        /// Folders; read: one folder is selected inside another one; use placeholder %1.
        /// </summary>
        DirectoryItems = 2,

        /// <summary>
        /// Folders; read: no file selected inside a folder, right click on empty space; use placeholder %V.
        /// </summary>
        DirectoryBackground = 3,
    }
}
