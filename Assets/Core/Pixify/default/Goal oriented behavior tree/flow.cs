using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra.Spirit
{
    public class flow : thought.chain
    {
        int ptr;
        chain main;
        chain [] o;

        protected override void OnAquire()
        {
            ptr = 0;
            main = o [ptr];
            o[ptr].Aquire (this);
        }

        public flow ( chain [] thoughts )
        {
            o = thoughts;
        }

        protected override bool OnGuestSelfFree(thought guest)
        {
            ptr ++;
            if ( ptr >= o.Length )
                return true;

            main = o [ptr];
            o[ptr].Aquire (this);
            return false;
        }

        public void Substitute ( chain thought )
        {
            if (!on) return;
            
            main.Free (this);
            main = thought;
            thought.Aquire (this);
        }

        protected override void OnFree()
        {
            main.Free (this);
        }
    }
}