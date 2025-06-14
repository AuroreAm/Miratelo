using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_lock_target : action
    {
        [Depend]
        m_actor ma;
        [Depend]
        c_ground_movement_lateral cgml;

        protected override bool Step()
        {
            if (ma.target != null)
            cgml.rotDir = ma.target.ms.Coord.position - ma.ms.Coord.position;

            return false;
        }
    }
}
