using System;
using System.Collections.Generic;
using System.Text;

namespace ShellStaticContextMenuManager.Model
{
    /// <summary>
    /// Model for a verb that cascades, thus, shows child items in a submenu when selected.
    /// </summary>
    public class CascadingVerb : Verb
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CascadingVerb"/> class.
        /// </summary>
        /// <param name="name">The value for the <see cref="Verb.Name"/> property.</param>
        /// <param name="muiVerb">The value for the <see cref="Verb.MuiVerb"/> property.</param>
        /// <param name="children">The value for the <see cref="Children"/> property.</param>
        public CascadingVerb(string name, string muiVerb, VerbCollection children)
            : base(name, muiVerb)
        {
            Children = children;
        }

        /// <summary>
        /// Gets the collection of child verb items that appear when this menu item is selected.
        /// </summary>
        public VerbCollection Children { get; }
    }
}
