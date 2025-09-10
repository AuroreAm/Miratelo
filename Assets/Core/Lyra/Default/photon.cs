using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public sealed class photon : moon
    {
        List < gem > gems = new List<gem> ();

        protected override void _ready()
        {
            system._new_member += _new_member;
        }

        void _new_member ( moon item )
        {
            if ( item is gem g )
            gems.Add ( g );
        }

        public void radiate <T> ( T gleam ) where T : struct
        {
            foreach (var i in gems)
            (i as pearl<T>) ? ._radiate ( gleam );
        }

        public interface gem {}
    }

    public interface pearl <T> : photon.gem where T : struct
    {
        public void _radiate ( T gleam );
    }
}