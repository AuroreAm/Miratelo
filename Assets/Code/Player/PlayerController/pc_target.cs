using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// after player getting a target, transition to: target
    /// </summary>
    [Unique]
    public class t_target : action
    {
        [Depend]
        m_actor ma;
        public float Distance = 20;

        protected override bool Step()
        {

            if ( ma.target != null )
            selector.CurrentSelector.SwitchTo ( StateKey2.targetting );

            return false;
        }
    }

    [Unique]
    public class pc_target : action
    {
        [Depend]
        m_actor ma;
        public float Distance = 20;

        protected override bool Step()
        {
            if (Player.GetButtonDown (BoutonId.R))
            {
                ma.UnlockTarget ();
                ma.LockATarget ( ma.GetNearestFacedFoe (Distance) );
                return false;
            }

            if ( Player.GetButtonDown ( BoutonId.R ) )
                ma.UnlockTarget ();

            return false;
        }
    }

    /// <summary>
    /// after the target does not exist or untarget switch to: fallback
    /// </summary>
    [Unique]
    public class t_untarget : action
    {
        [Depend]
        m_actor ma;

        protected override bool Step()
        {
            if ( ma.target == null )
            selector.CurrentSelector.FallBack ();

            return false;
        }
    }

}
