using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_target : reflexion
    {
        [Depend]
        d_actor da;
        public float Distance = 20;

        protected override void Step()
        {
            if ( Player.Focus.OnActive && !da.target )
            {
                da.UnlockTarget ();
                da.LockATarget ( da.GetNearestFacedFoe (Distance) );
                return;
            }

            if ( Player.Focus.OnActive && da.target )
                da.UnlockTarget ();
        }
    }
}
