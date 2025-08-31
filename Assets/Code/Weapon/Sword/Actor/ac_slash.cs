using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_slash : motor
    {
        public override int Priority => Pri.Action;

        [Depend]
        d_actor da;
        [Depend]
        s_sword_user ssu;
        [Depend]
        s_skin ss;
        [Depend]
        d_slash_skin_meta dssm;

        term SlashKey;

        public ac_slash(term SlashAnimation)
        {
            SlashKey = SlashAnimation;
        }

        protected override void Start()
        {
            BeginSlash();
        }

        void BeginSlash ( )
        {
            ss.PlayState (0, SlashKey, 0.1f * Time.timeScale, EndSlash, null, Slash);
            SendSlashSignal ();
        }

        void Slash ()
        {
            a_slash_attack.Fire ( new term ( ssu.Weapon.SlashName ), ssu.Weapon, dssm.Paths[SlashKey], ss.DurationOfState (SlashKey) - ss.EventPointsOfState (SlashKey) [0] );
        }

        void SendSlashSignal ()
        {
            Collider [] NearbyColliders;
            NearbyColliders = Physics.OverlapSphere ( ssu.Weapon.transform.position, ssu.Weapon.Length, Vecteur.Character );

            foreach (Collider col in NearbyColliders)
            {
                if ( Element.Contains (col.id ()) && Element.ElementActorIsNotAlly ( col.id (), da.faction )  )
                Element.SendMessage ( col.id(), new incomming_slash ( da.term, SlashKey, ss.DurationOfState (SlashKey) - ss.EventPointsOfState (SlashKey) [1]) ) ;
            }
        }

        void EndSlash ()
        {
            SelfStop();
        }
    }

    public struct incomming_slash
    {
        public term Sender;
        public term Slash;
        public float Duration;

        public incomming_slash ( term sender, term slash, float Duration )
        {
            this.Sender = sender;
            this.Slash = slash;
            this.Duration = Duration;
        }
    }
}