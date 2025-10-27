using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [RequireComponent (typeof (ParticleSystem))]
    public class IllusionAuthor : VirtusAuthor
    {
        protected override void _virtus_create()
        {
            new illusion.ink ( Instantiate (GetComponent <ParticleSystem> ()) );
        }

        illusion.w w;

        public illusion.w get_w () => bridge_cache ( ref w );
    }
}
