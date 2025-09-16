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

        public void _create()
        {
            new ink < virtus > ();
            _virtus_create ();
        }

        public void _created(system s)
        {}

        protected abstract void _virtus_create ();
    }

    
    public abstract class virtus_creator_simple : creator, virtus_creator
    {
        public virtus instance ()
        {
            var b = new system.creator ( this ).create_system ();
            return b.get <virtus> ();
        }

        public void _create()
        {
            new ink < virtus > ();
            _virtus_create ();
        }

        public void _created(system s)
        {}

        protected abstract void _virtus_create ();
    }

    public interface virtus_creator
    {
        public virtus instance ();
    }
}
