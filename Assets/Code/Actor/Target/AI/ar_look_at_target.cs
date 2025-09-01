using Lyra;
using Lyra.Spirit;

namespace Triheroes.Code
{
    [Category ("actor")]
    public class r_look_target : reflexion
    {
        [Depend]
        t_look_at_target tlt;

        [Depend]
        ac_look_at_target alat;

        protected override void Reflex()
        {
            if (tlt.on)
            {
                alat.MaxDeltaAngle = tlt.MaxDeltaAngle;
                Stage.Start ( alat );
                Stage.Start ( this );
            }
        }

        protected override void Step()
        {
            if (!alat.on)
            SelfStop ();
        }

        protected override void Stop()
        {
            tlt.Finish ();
        }
    }

    public class t_look_at_target : thought.final
    {
        public float MaxDeltaAngle {private set; get;}

        [Category ("actor")]
        public class look_at_target : package.o <t_look_at_target>
        {
            [Export]
            public float MaxDeltaAngle = 160;

            protected override void BeforeStart()
            {
                main.MaxDeltaAngle = MaxDeltaAngle;
            }
        }
    }
}