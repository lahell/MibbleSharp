﻿//
// DefaultContext.cs
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
using MibbleSharp.Value;
using MibbleSharp.Type;

namespace MibbleSharp
{
    /**
     * A default MIB context.
     *
     * @author   Per Cederberg, <per at percederberg dot net>
     * @version  2.8
     * @since    2.0
     */
    public class DefaultContext : MibContext
    {

        /**
         * The root "ccitt" symbol name.
         */
        public const string CCITT = "ccitt";

        /**
         * The root "iso" symbol name.
         */
        public const string ISO = "iso";

        /**
         * The root "joint-iso-ccitt" symbol name.
         */
        public const string JOINT_ISO_CCITT = "joint-iso-ccitt";

        /**
         * The map of default symbols.
         */
        private Dictionary<string, MibSymbol> symbols = new Dictionary<string, MibSymbol>();

        /**
         * Creates a new default context.
         */
        public DefaultContext()
        {
            initialize();
        }

        /**
         * Initializes this context by creating all default symbols.
         */
        private void initialize()
        {
            MibSymbol symbol;
            ObjectIdentifierValue oid;

            // Add the ccitt symbol
            oid = new ObjectIdentifierValue(CCITT, 0);
            symbol = new MibValueSymbol(new FileLocation(null, -1, -1),
                                        null,
                                        CCITT,
                                        new ObjectIdentifierType(),
                                        oid);
            oid.setSymbol((MibValueSymbol)symbol);
            symbols.Add(CCITT, symbol);

            // Add the iso symbol
            oid = new ObjectIdentifierValue(ISO, 1);
            symbol = new MibValueSymbol(new FileLocation(null, -1, -1),
                                        null,
                                        ISO,
                                        new ObjectIdentifierType(),
                                        oid);
            oid.setSymbol((MibValueSymbol)symbol);
            symbols.Add(ISO, symbol);

            // Add the joint-iso-ccitt symbol
            oid = new ObjectIdentifierValue(JOINT_ISO_CCITT, 2);
            symbol = new MibValueSymbol(new FileLocation(null, -1, -1),
                                        null,
                                        JOINT_ISO_CCITT,
                                        new ObjectIdentifierType(),
                                        oid);
            oid.setSymbol((MibValueSymbol)symbol);
            symbols.Add(JOINT_ISO_CCITT, symbol);
        }

        /**
         * Searches for a named MIB symbol. This method may search outside
         * the normal (or strict) scope, thereby allowing a form of
         * relaxed search. Note that the results from the normal and
         * expanded search may not be identical, due to the context
         * chaining and the same symbol name appearing in various
         * contexts.<p>
         *
         * <strong>NOTE:</strong> This is an internal method that should
         * only be called by the MIB loader.
         *
         * @param name           the symbol name
         * @param expanded       the expanded scope flag
         *
         * @return the MIB symbol, or null if not found
         *
         * @since 2.4
         */
        public MibSymbol findSymbol(string name, bool expanded)
        {
            if(symbols.ContainsKey(name))
                return symbols[name];
            return null;
        }

        /**
         * Returns a string representation of this object.
         *
         * @return a string representation of this object
         */
        public override string ToString()
        {
            return "<defaults>";
        }
    }

}