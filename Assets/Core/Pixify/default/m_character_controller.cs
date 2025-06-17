using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    // holds and manage modules
    // NOTE: avoid depending on this module as this need to be the last module in the character
    public class m_character_controller : core
    {
        action root;

        public override void Main()
        {
            if (root.on)
                root.iExecute();
            if (!root.on)
                enabled = false;
        }

        protected override void OnFree()
        {
            if (root.on)
                root.iAbort();
        }

        /// <summary>
        /// set the root and immediately start
        /// </summary>
        public void StartRoot (action root)
        {
            if ( on && this.root.on )
                this.root.iAbort();

            if ( root == null )
            throw new InvalidOperationException ("FATAL ERROR: start root where root is null");

            this.root = root;
            root.iStart ();
            enabled = true;

            if (!on)
            Aquire (new Void ());
        }

    }
}
