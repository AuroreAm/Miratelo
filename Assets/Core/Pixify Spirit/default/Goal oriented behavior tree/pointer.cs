using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class pointer : thought.chain
    {
        term to;

        [Depend]
        s_mind sm;

        chain main;

        public pointer (term to)
        {
            this.to = to;
        }

        protected override void OnAquire()
        {
            main = sm.GetThought (to);
            main.Aquire (this);
        }

        protected override void OnFree()
        {
            main.Free (this);
        }
    }
}