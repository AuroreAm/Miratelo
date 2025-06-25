using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_target : reflection
    {
        [Depend]
        m_actor ma;
        public float Distance = 20;

        public override void Main()
        {
            if ( Player.Focus.OnActive && !ma.target )
            {
                ma.UnlockTarget ();
                ma.LockATarget ( ma.GetNearestFacedFoe (Distance) );
                return;
            }

            if ( Player.Focus.OnActive && ma.target )
                ma.UnlockTarget ();
        }
    }
}
