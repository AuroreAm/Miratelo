using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_aim : skill_reflexion <BS0>
    {
        [Depend]
        pc_lateral_move plm;
        
        [Depend]
        ac_aim aa;

        protected override void SkillReflex ( BS0 skill )
        {
            if ( Player.Aim.Active )
            {
                skill.Start ();
                Stage.Start (this);
            }
        }
        
        public override void Create()
        {
            Link (plm);
        }

        protected override void Step ()
        {
            aa.Aim ( s_camera.o.td.rotX, s_camera.o.td.rotY );

            if (!aa.on)
            {
            SelfStop ();
            return;
            }
            
            if (Player.Aim.OnRelease)
            {
            skill.Stop ();
            SelfStop ();
            }
        }
    }
}
