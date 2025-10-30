using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [RequireComponent (typeof (ParticleSystem))]
    public class IllusionAuthor : VirtusAuthor <illusion.w>
    {
        protected override void _virtus_create()
        {
            new illusion.ink ( Instantiate (GetComponent <ParticleSystem> ()) );
        }
    }
}
