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

        int ComboId = 0;

        public ac_slash (int ComboID)
        {
            ComboId = ComboID;
        }

        protected override void Start()
        {
            BeginSlash(ComboId);
        }

        void BeginSlash ( int id )
        {
            ss.PlayState (0, s_sword_user.SlashKeys[id], 0.1f, EndSlash, null, Slash);
            SendSlashSignal ();
        }

        void Slash ()
        {
            a_slash_attack.Fire ( new term ( ssu.Weapon.SlashName ), ssu.Weapon, ss.EventPointsOfState ( s_sword_user.SlashKeys[ComboId] ) [1] - ss.EventPointsOfState ( s_sword_user.SlashKeys[ComboId] ) [0] );
        }

        void SendSlashSignal ()
        {
            Collider [] NearbyColliders;
            NearbyColliders = Physics.OverlapSphere ( ssu.Weapon.transform.position, ssu.Weapon.Length, Vecteur.Character );

            foreach (Collider col in NearbyColliders)
            {
                if ( Element.Contains (col.id ()) )
                Element.SendMessage ( col.id(), Signal.incomming_slash, new incomming_slash ( da.term, true ) );
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