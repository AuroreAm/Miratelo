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

    public class ac_look_at_target : action
    {
        [Depend]
        m_actor ma;
        [Depend]
        m_skin ms;

        public float MaxDeltaAngle = 160;

        protected override bool Step()
        {
            var rotDir = Vecteur.RotDirection (ma.md.position,ma.target.md.position);
            ms.rotY = new Vector3 (0, Mathf.MoveTowardsAngle(ms.rotY.y, rotDir.y, Time.deltaTime * MaxDeltaAngle), 0);
            
            if (rotDir.y == ms.rotY.y)
            return true;

            return false;
        }
    }
}
