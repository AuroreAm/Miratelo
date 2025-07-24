using System.Collections;
using System.Collections.Generic;
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
            a_slash_attack.Fire ( new term ( ssu.Weapon.SlashName ), ssu.Weapon, ss.EventPointsOfState ( SlashKey ) [1] - ss.EventPointsOfState ( SlashKey ) [0] );
        }

        void SendSlashSignal ()
        {
            Collider [] NearbyColliders;
            NearbyColliders = Physics.OverlapSphere ( ssu.Weapon.transform.position, ssu.Weapon.Length, Vecteur.Character );

            foreach (Collider col in NearbyColliders)
            {
                if ( Element.Contains (col.id ()) )
                Element.SendMessage ( col.id(), new incomming_slash ( da.term, true ) );
            }
        }

        void EndSlash ()
        {
            SelfStop();
        }
    }

    public struct incomming_slash
    {
        public term sender;
        public bool MostLikelyHit;

        public incomming_slash ( term sender, bool MostLikelyHit )
        {
            this.sender = sender;
            this.MostLikelyHit = MostLikelyHit;
        }
    }
}