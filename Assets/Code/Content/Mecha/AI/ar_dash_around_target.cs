using Pixify.Spirit;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ar_dash_around_target : skill_reflexion <DS0_dash>
    {

        [Depend]
        t_dash_around_target tdat;

        [Depend]
        d_actor da;

        [Depend]
        s_skin ss;

        [Depend]
        d_ground dg;

        protected override void SkillReflex(DS0_dash skill)
        {
            if (da.target && tdat.on)
            {
                float RotYCache = ss.rotY;

                ss.rotY =  Vecteur.RotDirectionY ( da.dd.position,da.target.dd.position ) + tdat.direction;

                if (skill.Spam ( direction.forward ))
                {
                    dg.rotY = ss.rotY;
                    Stage.Start (this);
                }

                ss.rotY = RotYCache;
            }
        }

        protected override void Step()
        {
            if ( !skill.Active || !da.target )
            {
                tdat.Finish ();
                SelfStop ();
            }
        }
    }
}