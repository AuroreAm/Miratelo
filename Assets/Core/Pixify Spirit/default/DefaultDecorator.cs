using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{

    public sealed class sequence : decorator
    {
        int ptr;

        [Export]
        public bool repeat = true;
        [Export]
        public bool reset = true;

        protected sealed override void Start ()
        {
            if (reset)
            ptr = 0;
            o[ptr].Tick ( this );
        }

        protected sealed override void Step ()
        {
            o [ptr].Tick (this);
        }

        protected override void Abort()
        {
            if (o[ptr].on)
            o[ptr].ForceStop (this);
        }

        public override void OnPixiEnd(pixi p)
        {
            if (!on)
            return;

            ptr++;
            if (ptr >= o.Length)
            {
                ptr = 0;
                if (!repeat)
                {
                    SelfStop();
                    return;
                }
            }

            o[ptr].Tick ( this );
        }
    }

}