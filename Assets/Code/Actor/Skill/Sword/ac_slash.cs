using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public class ac_slash : motor
    {
        public override int Priority => Pri.Action;

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
        }

        void Slash ()
        {
            a_slash_attack.Fire ( new term ( ssu.Weapon.SlashName ), ssu.Weapon, ss.EventPointsOfState ( s_sword_user.SlashKeys[ComboId] ) [1] - ss.EventPointsOfState ( s_sword_user.SlashKeys[ComboId] ) [0] );
        }

        void EndSlash ()
        {
            SelfStop();
        }
    }
}