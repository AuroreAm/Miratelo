using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class flow : thought
    {
        int ptr;
        thought [] o;

        protected override void OnAquire()
        {
            ptr = 0;

            o[ptr].Aquire (this);
        }

        protected override bool OnGuestSelfFree(thought guest)
        {
            ptr ++;
            if ( ptr >= o.Length )
                return true;
            o[ptr].Aquire (this);
            return false;
        }
    }
}