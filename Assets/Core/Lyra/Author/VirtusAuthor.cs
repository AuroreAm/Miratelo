using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class VirtusAuthor : MonoBehaviour, creator, virtus_creator
    {
        public virtus instance()
        {
            var b = new system.creator ( this ).create_system ();
            return b.get <virtus> ();
        }

        public void _create ()
        {
            new ink < virtus > ();
            _virtus_create ();
        }

        protected abstract void _virtus_create ();

        public void Start ()
        {
            Destroy (this);
        }

        public virtual void _created(system s)
        {}

        protected T get_bridge <T> ( ref T w ) where T : bridge, new () {
            if (w == null) {
                var n = new term (name);
                w = bridge.create <T> ( n );
                orion.add ( this, name );
            }
            
            return w;
        }
    }
}
