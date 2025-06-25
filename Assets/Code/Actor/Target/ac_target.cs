using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class ac_get_a_target : action
    {
        [Depend]
        m_actor ma;

        public float Distance = 30;

        protected override bool Step()
        {
            ma.LockATarget ( ma.GetNearestFacedFoe ( Distance ) );
            if ( ma.target )
            return true;
            return false;
        }
    }

    [Unique]
    public class ac_have_target : action
    {
        [Depend]
        m_actor ma;

        protected override bool Step()
        {
            if ( ma.target )
            return false;
            return true;
        }
    }
}
