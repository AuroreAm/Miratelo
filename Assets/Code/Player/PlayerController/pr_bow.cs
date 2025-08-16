using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_aim : reflexion
    {
        [Depend]
        d_skill ds;

        [Depend]
        pc_aim pa;

        protected override void Step()
        {
            if (!ds.SkillValid <BS0> () ) return;

            if ( Player.Aim.Active && !ds.GetSkill <BS0> ().SkillActive)
            ds.GetSkill <BS0> ().Start ();

            if ( !ds.GetSkill <BS0> ().SkillActive) return;

            if (!pa.on)
            Stage.Start (pa);

            if (Player.Aim.OnRelease)
            ds.GetSkill <BS0> ().Stop ();
        }
    }

    public class pc_aim : action
    {
        
        [Depend]
        pc_lateral_move plm;
        
        [Depend]
        ac_aim aa;
        
        [Depend]
        s_bow_user sbu;

        [Depend]
        character c;

        public override void Create()
        {
            Link (plm);
        }

        protected override void Step ()
        {
            Vector3 RotDirection = s_camera.o.td.rotY;
            aa.Aim ( RotDirection );

            if (!aa.on)
            SelfStop ();
        }
    }
}
