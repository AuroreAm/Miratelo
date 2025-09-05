using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class VirtusCreator : ScriptableObject, creator, virtus_creator
    {
        public virtus instance ()
        {
            var b = new system.creator ( this ).create_system ();
            return b.get <virtus> ();
        }

        public void _creation()
        {
            new ink < virtus > ();
            _virtus_creation ();
        }

        protected abstract void _virtus_creation ();
    }

    
    public interface virtus_creator
    {
        public virtus instance ();
    }
}
