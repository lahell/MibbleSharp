﻿//
// SequenceType.cs
// 
// This work is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published
// by the Free Software Foundation; either version 2 of the License,
// or (at your option) any later version.
//
// This work is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307
// USA
// 
// Original Java code Copyright (c) 2004-2016 Per Cederberg. All
// rights reserved.
// C# conversion Copyright (c) 2016 Jeremy Gibbons. All rights reserved
//

using System.Collections.Generic;
using System.Linq;


namespace MibbleSharp.Type
{
    /**
     * A sequence MIB type. In some other languages this is known as a
     * struct.
     *
     * @author   Per Cederberg, <per at percederberg dot net>
     * @version  2.2
     * @since    2.0
     */
    public class SequenceType : MibType
    {

        /**
         * The sequence elements.
         */
        private IList<ElementType> elements;

        /**
         * Creates a new sequence MIB type.
         *
         * @param elements       the list of element types
         */
        public SequenceType(IList<ElementType> elements) : this(true, elements)
        {
        }

        /**
         * Creates a new sequence MIB type.
         *
         * @param primitive      the primitive type flag
         * @param elements       the list of element types
         */
        private SequenceType(bool primitive, IList<ElementType> elements) : base("SEQUENCE", primitive)
        {
            this.elements = elements;
            setTag(true, MibTypeTag.SEQUENCE);
        }

        /**
         * Initializes the MIB type. This will remove all levels of
         * indirection present, such as references to types or values. No
         * information is lost by this operation. This method may modify
         * this object as a side-effect, and will return the basic
         * type.<p>
         *
         * <strong>NOTE:</strong> This is an internal method that should
         * only be called by the MIB loader.
         *
         * @param symbol         the MIB symbol containing this type
         * @param log            the MIB loader log
         *
         * @return the basic MIB type
         *
         * @throws MibException if an error was encountered during the
         *             initialization
         *
         * @since 2.2
         */
        public override MibType Initialize(MibSymbol symbol, MibLoaderLog log)
        {
            elements.Select(e => e.Initialize(symbol, log));
            return this;
        }

        /**
         * Creates a type reference to this type. The type reference is
         * normally an identical type, but with the primitive flag set to
         * false. Only certain types support being referenced, and the
         * default implementation of this method throws an exception.<p>
         *
         * <strong>NOTE:</strong> This is an internal method that should
         * only be called by the MIB loader.
         *
         * @return the MIB type reference
         *
         * @since 2.2
         */
        public override MibType CreateReference()
        {
            SequenceType type = new SequenceType(false, elements);

            type.setTag(true, getTag());
            return type;
        }

        /**
         * Checks if the specified value is compatible with this type. No
         * values are considered compatible with this type, and this
         * method therefore always returns false.
         *
         * @param value          the value to check
         *
         * @return true if the value is compatible, or
         *         false otherwise
         */
        public override bool IsCompatible(MibValue value)
        {
            return false;
        }

        /**
         * Returns all the element types. These are the types that the
         * sequence type is composed of.
         *
         * @return an array of the element types
         *
         * @since 2.2
         */
        public IEnumerable<ElementType> getAllElements()
        {
            return elements;
        }

        /**
         * Returns a string representation of this object.
         *
         * @return a string representation of this object
         */
        public override string ToString()
        {
            return base.ToString() + " " + elements.ToString();
        }
    }

}
