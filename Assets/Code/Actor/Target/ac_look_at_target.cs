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
            dg.rotY = Vecteur.RotDirectionY ( da.dd.position,da.target.dd.position );
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
            var rotYDir = Vecteur.RotDirectionY (da.dd.position,da.target.dd.position);
            ss.rotY = Mathf.MoveTowardsAngle(ss.rotY, rotYDir, Time.deltaTime * MaxDeltaAngle);
            
            if (rotYDir == ss.rotY)
            SelfStop ();
        }
    }
}
