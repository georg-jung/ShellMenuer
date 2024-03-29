﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShellMenuer.Model
{
    /// <summary>
    /// Model for a registry class, that models the <see cref="Verb"/>s for this class. A class can be e.g.
    /// "*", "Directory", "Directory\Background", "image", ".txt", ".log".
    /// </summary>
    public class Class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Class"/> class.
        /// </summary>
        /// <param name="identifier">The value for the <see cref="Identifier"/> property.</param>
        public Class(string identifier)
        {
            Identifier = identifier;
        }

        /// <summary>
        /// Gets the value that identifies this class, eg. "*", "Directory", "txtfile, ".txt", ".log".
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// Gets a collection of the <see cref="Verb"/>s that are associated with this class.
        /// </summary>
        public ICollection<Verb> Verbs { get; } = new List<Verb>();
    }
}
