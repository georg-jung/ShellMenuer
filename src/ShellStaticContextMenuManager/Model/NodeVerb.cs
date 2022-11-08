using System;
using System.Collections.Generic;
using System.Text;

namespace ShellStaticContextMenuManager.Model
{
    /// <summary>
    /// Model for a verb that represents some clickable action on a file or a folder.
    /// </summary>
    public class NodeVerb : Verb
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeVerb"/> class.
        /// </summary>
        /// <param name="name">The value for the <see cref="Verb.Name"/> property.</param>
        /// <param name="muiVerb">The value for the <see cref="Verb.MuiVerb"/> property.</param>
        /// <param name="commandText">The value for the <see cref="CommandText"/> property.</param>
        public NodeVerb(string name, string muiVerb, string commandText)
            : base(name, muiVerb)
        {
            CommandText = commandText;
        }

        /// <summary>
        /// Gets the command that should be run when executing this verb, e.g. 'cmd.exe "%1"'.
        /// </summary>
        public string CommandText { get; }
    }
}
