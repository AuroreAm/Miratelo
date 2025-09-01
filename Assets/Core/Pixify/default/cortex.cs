using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra.Spirit
{
    public abstract class cortex : pix
    {
        [Depend]
        s_mind sm;

        public abstract void Setup ();

        protected void AddReflexion <T> () where T : reflexion, new ()
        {
            sm.AddReflexion ( b.RequirePix <T> () );
        }
    } 
}
