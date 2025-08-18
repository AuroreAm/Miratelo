using Pixify;

namespace Triheroes.Code
{
    public class ar_ms_SS4 : skill_reflexion <MSS4>
    {
        [Depend]
        t_ms_SS4 tmSS4;

        protected override void SkillReflex(MSS4 skill)
        {
            if ( tmSS4.on )
            {
                if (skill.Spam ())
                Stage.Start (this);
            }
        }

        protected override void Step()
        {
            if (!skill.Active)
            SelfStop ();
        }

        protected override void Stop()
        {
            tmSS4.Finish ();
        }
    }
}