using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ar_mb_aim_target : skill_reflexion <MBS0>
    {
        [Depend]
        t_mb_aim_target tmat;

        [Depend]
        d_actor da;

        [Depend]
        ac_mb_aim ama;
        
        [Depend]
        d_mecha_buster dmb;

        protected override void SkillReflex(MBS0 skill)
        {
            if (tmat.on && da.target)
            {
                if (skill.Start ())
                Stage.Start (this);
            }
        }

        protected override void Step()
        {
            if ( !da.target || !tmat.on || !skill.Active )
            {
                SelfStop ();
                return;
            }

            ama.Aim ( Vecteur.RotDirectionY ( dmb.BusterOriginPosition,  da.target.dd.position ) );

            if (AimingEnoughAtTarget ())
            {
                SelfStop ();
                tmat.Finish ();
            }
        }

        protected override void Stop()
        {
            skill.Stop ();
        }

        bool AimingEnoughAtTarget ()
        {
            return Mathf.Abs ( Mathf.DeltaAngle (dmb.rotY, Vecteur.RotDirectionY ( dmb.BusterOriginPosition,  da.target.dd.position )) ) < 10;
        }
    }
}