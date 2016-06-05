// <copyright file="ProductionPattern.cs" company="None">
//    <para>
//    This program is free software: you can redistribute it and/or
//    modify it under the terms of the BSD license.</para>
//    <para>
//    This work is distributed in the hope that it will be useful, but
//    WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.</para>
//    <para>
//    See the LICENSE.txt file for more details.</para>
//    Original code as generated by Grammatica 1.6 Copyright (c) 
//    2003-2015 Per Cederberg. All rights reserved.
//    Updates Copyright (c) 2016 Jeremy Gibbons. All rights reserved
// </copyright>

namespace PerCederberg.Grammatica.Runtime
{
    using System.Collections;
    using System.Text;

    /// <summary>
    /// A production pattern. This class represents a set of production
    /// alternatives that together forms a single production. A
    /// production pattern is identified by an integer id and a name,
    /// both provided upon creation. The pattern id is used for
    /// referencing the production pattern from production pattern
    /// elements.
    /// </summary>
    public class ProductionPattern
    {
        /// <summary>
        /// The production pattern identity.
        /// </summary> 
        private int id;

        /// <summary>
        /// The production pattern name.
        /// </summary> 
        private string name;

        /// <summary>
        /// The synthetic production flag. If this flag is set, the
        /// production identified by this pattern has been artificially
        /// inserted into the grammar.
        /// </summary> 
        private bool synthetic;

        /// <summary>
        /// The list of production pattern alternatives.
        /// </summary>
        private ArrayList alternatives;

        /// <summary>
        /// The default production pattern alternative. This alternative
        /// is used when no other alternatives match. It may be set to
        /// -1, meaning that there is no default (or fallback) alternative.
        /// </summary> 
        private int defaultAlt;

        /// <summary>
        /// The look-ahead set associated with this pattern.
        /// </summary> 
        private LookAheadSet lookAhead;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionPattern"/> class.
        /// </summary>
        /// <param name="id">The production pattern id</param>
        /// <param name="name">The production pattern name</param>
        public ProductionPattern(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.synthetic = false;
            this.alternatives = new ArrayList();
            this.defaultAlt = -1;
            this.lookAhead = null;
        }

        /// <summary>
        /// Gets the production pattern identity (read-only). This
        /// property contains the unique identity value.
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        /// Gets the production pattern name (read-only).
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is a synthetic 
        /// production pattern. If this property is set, the production 
        /// identified by this pattern has been artificially inserted 
        /// into the grammar. No parse tree nodes will be created for 
        /// such nodes, instead the child nodes will be added directly 
        /// to the parent node. By default this property is set to false.
        /// </summary>
        public bool Synthetic
        {
            get
            {
                return this.synthetic;
            }

            set
            {
                this.synthetic = value;
            }
        }

        /// <summary>
        /// Gets the production pattern alternative count property
        /// (read-only).
        /// </summary>
        public int Count
        {
            get
            {
                return this.alternatives.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this pattern is recursive 
        /// on the left-hand side. This method checks if any of the 
        /// production pattern alternatives is left-recursive.
        /// </summary>
        /// <returns>
        /// True if at least one alternative is left recursive, or false otherwise
        /// </returns>
        public bool IsLeftRecursive
        {
            get
            {
                ProductionPatternAlternative alt;

                for (int i = 0; i < this.alternatives.Count; i++)
                {
                    alt = (ProductionPatternAlternative)this.alternatives[i];
                    if (alt.IsLeftRecursive)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this pattern is recursive 
        /// on the right-hand side. This method checks if any of the 
        /// production pattern alternatives is right-recursive.
        /// </summary>
        public bool IsRightRecursive
        {
            get
            {
                ProductionPatternAlternative alt;

                for (int i = 0; i < this.alternatives.Count; i++)
                {
                    alt = (ProductionPatternAlternative)this.alternatives[i];
                    if (alt.IsRightRecursive)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this pattern would match an empty stream of
        /// tokens. This method checks if any one of the production
        /// pattern alternatives would match the empty token stream.
        /// </summary>
        public bool IsMatchingEmpty
        {
            get
            {
                ProductionPatternAlternative alt;

                for (int i = 0; i < this.alternatives.Count; i++)
                {
                    alt = (ProductionPatternAlternative)this.alternatives[i];
                    if (alt.IsMatchingEmpty)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets or sets the look-ahead set property. This property contains the
        /// look-ahead set associated with this alternative.
        /// </summary> 
        internal LookAheadSet LookAhead
        {
            get
            {
                return this.lookAhead;
            }

            set
            {
                this.lookAhead = value;
            }
        }

        /// <summary>
        /// Gets or sets the default pattern alternative. The default
        /// alternative is used when no other alternative matches. The
        /// default alternative must previously have been added to the
        /// list of alternatives. This property is set to null if no
        /// default pattern alternative has been set.
        /// </summary> 
        internal ProductionPatternAlternative DefaultAlternative
        {
            get
            {
                if (this.defaultAlt >= 0)
                {
                    object obj = this.alternatives[this.defaultAlt];
                    return (ProductionPatternAlternative)obj;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                this.defaultAlt = 0;
                for (int i = 0; i < this.alternatives.Count; i++)
                {
                    if (this.alternatives[i] == value)
                    {
                        this.defaultAlt = i;
                    }
                }
            }
        }

        /// <summary>
        /// The production pattern alternative index (read-only).
        /// </summary>
        /// <param name="index">The alternative index, from 0 to Count</param>
        /// <returns>The production pattern alternative at the given index</returns>
        public ProductionPatternAlternative this[int index]
        {
            get
            {
                return (ProductionPatternAlternative)this.alternatives[index];
            }
        }

        /// <summary>
        /// Adds a production pattern alternative.
        /// </summary>
        /// <param name="alt">The production pattern alternative to add</param>
        /// <exception cref="ParserCreationException">
        /// If an identical alternative has already been added
        /// </exception>
        public void AddAlternative(ProductionPatternAlternative alt)
        {
            if (this.alternatives.Contains(alt))
            {
                throw new ParserCreationException(
                    ParserCreationException.ErrorType.InvalidProduction,
                    this.name,
                    "two identical alternatives exist");
            }

            alt.Pattern = this;
            this.alternatives.Add(alt);
        }

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>
        /// Token string representation
        /// </returns>
        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            StringBuilder indent = new StringBuilder();
            int i;

            buffer.Append(this.name);
            buffer.Append("(");
            buffer.Append(this.id);
            buffer.Append(") ");
            for (i = 0; i < buffer.Length; i++)
            {
                indent.Append(" ");
            }

            for (i = 0; i < this.alternatives.Count; i++)
            {
                if (i == 0)
                {
                    buffer.Append("= ");
                }
                else
                {
                    buffer.Append("\n");
                    buffer.Append(indent);
                    buffer.Append("| ");
                }

                buffer.Append(this.alternatives[i]);
            }

            return buffer.ToString();
        }
    }
}
