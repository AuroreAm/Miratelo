using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_MSS4 : motor
    {
        public override int Priority => Pri.Action;
        int state;

        [Depend]
        s_skin ss;
        [Depend]
        s_capsule_character_controller sccc;

        public override void Create()
        {
            dash = new delta_curve ( SubResources <CurveRes>.q ( new term ("jump") ).Curve );
            Link (sccc);
        }

        protected override void Start()
        {
            state = 0;
            ss.PlayState ( 0, AnimationKey.SS4, 0.1f, EndSlash, null, Dash, Slash );
        }

        protected override void Step()
        {
            if (state == 1)
            sccc.dir += Vecteur.LDir(ss.rotY,Vector3.forward) * dash.TickDelta ();
        }

        delta_curve dash;
        void Dash ()
        {
            dash.Start ( 5, ss.EventPointsOfState (AnimationKey.SS4) [1] - ss.EventPointsOfState (AnimationKey.SS4) [0] );
            state = 1;
        }

        void Slash ()
        {

        }
        
        void EndSlash ()
        {
            SelfStop ();
        }

        // TODO slash signal
    }

    [Category ("mecha skill")]
    public class MSS4 : skill_motor.First
    {
        [Depend]
        ac_MSS4 mss4;
        public bool Spam () => StartMotor ( mss4 );
    }
}