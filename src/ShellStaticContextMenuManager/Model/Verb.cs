using System;
using System.Collections.Generic;
using System.Text;

namespace ShellStaticContextMenuManager.Model
{
    /// <summary>
    /// Abstract model for a verb, representing a context menu item.
    /// </summary>
    public abstract class Verb
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Verb"/> class.
        /// </summary>
        /// <param name="name">The value for the <see cref="Name"/> property.</param>
        /// <param name="muiVerb">The value for the <see cref="MuiVerb"/> property.</param>
        public Verb(string name, string muiVerb)
        {
            Name = name;
            MuiVerb = muiVerb;
        }

        /// <summary>
        /// Gets the name of the verb, such as "open" or something custom.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the user-visible caption of the menu item.
        /// </summary>
        public string MuiVerb { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the verb should just be shown if the SHIFT key is pressed.
        /// </summary>
        public bool Extended { get; set; }

        /// <summary>
        /// Gets or sets a value describing the icon of the menu item, e.g. "cmd.exe" to use its icon
        /// or "someLib.dll,-1002" to use a specific icon.
        /// </summary>
        public string? Icon { get; set; }
    }
}
