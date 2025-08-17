using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{

    public sealed class ac_slash_hooker_up : motor
    {
        public override int Priority => Pri.Action;

        [Depend]
        s_sword_user ssu;
        [Depend]
        s_skin ss;

        [Depend]
        s_capsule_character_controller sccc;

        delta_curve cu;
        term SlashKey;

        readonly float duration = .5f;
        readonly float jumpHeight = 2;

        public ac_slash_hooker_up(term SlashAnimation)
        {
            SlashKey = SlashAnimation;
        }

        public override void Create()
        {
            Link ( sccc );
            cu = new delta_curve ( SubResources <CurveRes>.q ( new term ("jump") ).Curve );
            // base.create ();
        }

        protected override void Start()
        {
            cu.Start ( jumpHeight, duration );
            BeginSlash ();
        }

        void BeginSlash ( )
        {
            ss.PlayState (0, SlashKey, 0.1f * Time.timeScale, EndSlash, null, Slash);
        }

        protected override void Step()
        {
            sccc.dir += new Vector3 ( 0, cu.TickDelta (), 0 );
        }

        void Slash ()
        {
            a_hook_attack.Fire ( new term ( ssu.Weapon.HookSlashName ), ssu.Weapon, duration, cu.curve, Vector3.up );
        }


        void EndSlash ()
        {
            SelfStop();
        }
    }
}
