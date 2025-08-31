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
        d_actor da;

        [Depend]
        s_skin ss;

        [Depend]
        s_mecha_sword sms;

        [Depend]
        d_dimension dd;
        
        [Depend]
        d_slash_skin_meta dssm;

        [Depend]
        s_capsule_character_controller sccc;

        readonly term SlashKey = AnimationKey.SS4;

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
            dash.Start ( 5, ss.DurationOfState (AnimationKey.SS4) - ss.EventPointsOfState (AnimationKey.SS4) [0] );
            state = 1;
        }

        void Slash ()
        {
            SendSlashSignal ();

            a_slash_attack.Fire ( new term ( sms.MechaSword.SlashName ), sms.MechaSword, dssm.Paths [AnimationKey.SS4], ss.DurationOfState (AnimationKey.SS4) - ss.EventPointsOfState (AnimationKey.SS4) [1] );
        }

        void SendSlashSignal ()
        {
            RaycastHit [] ToSendSignal;
            ToSendSignal = Physics.SphereCastAll ( ss.position, dd.r, Vecteur.LDir ( ss.rotY, Vector3.forward ), 5, Vecteur.Character );

            foreach ( RaycastHit Hit in ToSendSignal )
            {
                if ( Element.Contains ( Hit.collider.id () ) && Element.ElementActorIsNotAlly ( Hit.collider.id (), da.faction ) )
                Element.SendMessage ( Hit.collider.id (), new incomming_slash ( da.term, SlashKey,ss.DurationOfState (AnimationKey.SS4) - ss.EventPointsOfState (AnimationKey.SS4) [1] ) );
            }
        }
        
        void EndSlash ()
        {
            SelfStop ();
        }
    }

    [Category ("mecha skill")]
    public class MSS4 : skill_motor.First
    {
        [Depend]
        ac_MSS4 mss4;
        public bool Spam () => StartMotor ( mss4 );
    }
}