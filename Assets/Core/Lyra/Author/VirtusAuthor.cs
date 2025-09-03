using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class VirtusAuthor : ScriptableObject, IStructureAuthor, IVirtusAuthor
    {
        public virtus Instance ()
        {
            var b = new dat.structure.Creator ( this ).CreateStructure ();
            return b.Get <virtus> ();
        }

        public void OnStructure()
        {
            dat.Q < virtus > ();
            OnVirtusStructure ();
        }

        protected abstract void OnVirtusStructure ();
    }

    
    public interface IVirtusAuthor
    {
        public virtus Instance ();
    }
}
