using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_lock_target : action
    {
        [Depend]
        d_actor da;
        [Depend]
        d_ground dg;

        protected override void Step()
        {
            if (da.target != null)
            dg.rotY.y = Vecteur.RotDirectionY ( da.dd.position,da.target.dd.position );
        }
    }

    public class ac_look_at_target : action
    {
        [Depend]
        d_actor da;
        [Depend]
        s_skin ss;

        public float MaxDeltaAngle = 160;

        protected override void Step()
        {
            var rotDir = Vecteur.RotDirection (da.dd.position,da.target.dd.position);
            ss.rotY = new Vector3 (0, Mathf.MoveTowardsAngle(ss.rotY.y, rotDir.y, Time.deltaTime * MaxDeltaAngle), 0);
            
            if (rotDir.y == ss.rotY.y)
            SelfStop ();
        }
    }
}
