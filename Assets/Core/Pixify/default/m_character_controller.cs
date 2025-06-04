using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    // holds and manage modules
    // Note: avoid depending on this module as this need to be the last module in the character
    public class m_character_controller : core
    {
        public action root;
        public override void Create()
        {}

        protected override void OnAquire()
        {
            root.iStart();
        }

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
    }
}
