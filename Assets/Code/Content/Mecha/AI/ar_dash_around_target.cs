using Pixify;
using Pixify.Spirit;

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

        int state;
        ac_look look;
        float TargetAngleDiff;

        public override void Create()
        {
            look = new ac_look ();
            b.IntegratePix (look);
        }

        protected override void SkillReflex(DS0_dash skill)
        {
            if (da.target && tdat.on)
                Stage.Start (this);
        }

        protected override void Start()
        {
            state = 1;
        }

        protected override void Step()
        {
            if ( !da.target || !tdat.on )
            {
                SelfStop ();
                return;
            }

            switch (state)
            {
                case 1:
                look.y = Vecteur.RotDirectionY ( da.dd.position,da.target.dd.position ) + tdat.direction;
                Stage.Start ( look );
                state ++;
                break;

                case 2:
                if ( !look.on )
                state ++;
                break;

                case 3:
                if (skill.Spam ( direction.forward ))
                state ++;
                else
                SelfStop ();
                break;

                case 4:
                dg.rotY = Vecteur.RotDirectionY ( da.dd.position,da.target.dd.position ) + tdat.direction;
                if ( !skill.Active )
                SelfStop ();
                break;
            }
        }

        protected override void Stop()
        {
            tdat.Finish ();
            state = 0;
        }
    }
}