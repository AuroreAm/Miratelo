using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class DotAuthor : ScriptableObject, IAuthor, IDotAuthor
    {
        public dot Instance ()
        {
            var b = new shard.constelation.write ( this ).constelation ();
            return b.get <dot> ();
        }

        public void writings()
        {
            new ink < dot > ();
            OnVirtusStructure ();
        }

        protected abstract void OnVirtusStructure ();
    }

    
    public interface IDotAuthor
    {
        public dot Instance ();
    }
}
