using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [RequireComponent (typeof (ParticleSystem))]
    public class IllusionAuthor : VirtusAuthor
    {
        protected override void _virtus_creation()
        {
            new illusion.ink ( GetComponent <ParticleSystem> () );
        }
    }
}
