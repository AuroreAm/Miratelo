using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_parry : motor
    {
        public override int Priority => Pri.Action;

        [Depend]
        d_actor da;
        [Depend]
        s_sword_user ssu;
        [Depend]
        s_skin ss;

        term SlashKey = AnimationKey.SS8_0;

        public ac_parry (term SlashAnimation)
        {
            SlashKey = SlashAnimation;
        }

        protected override void Start ()
        {
            BeginSlash ();
        }

        void BeginSlash ()
        {
            ss.PlayState ( 0, SlashKey, 0.1f, EndSlash, null, ActiveParry, DisableParry );
        }

        void ActiveParry ()
        {
            ssu.Weapon.OpenParry ();
        }

        void DisableParry ()
        {
            ssu.Weapon.CloseParry ();
        }

        protected override void Stop()
        {
            ssu.Weapon.CloseParry ();
        }

        void EndSlash ()
        {
            SelfStop ();
        }
    }

    public struct parried
    {}

}
