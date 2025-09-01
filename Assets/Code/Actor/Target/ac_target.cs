using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra.Spirit;
using Lyra;

namespace Triheroes.Code
{
    public class ac_get_a_target : action
    {
        [Depend]
        d_actor da;

        public float Distance = 30;

        protected override void Step()
        {
            da.LockATarget ( da.GetNearestFacedFoe ( Distance ) );
            if ( da.target )
            SelfStop ();
        }
    }
}
