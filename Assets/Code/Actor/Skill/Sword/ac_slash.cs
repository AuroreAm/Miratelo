using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_slash : motor, ITaskable
    {
        public override int Priority => Pri.Action;

        public SuperKey TaskID => m_sword_user.TaskIDS[ComboId];

        [Depend]
        m_sword_user msu;
        [Depend]
        m_skin ms;

        [Export]
        public int ComboId = 0;

        public void GetPreconditions(Dictionary<int, object> Preconditions)
        {
            // TODO change string to hash
            Preconditions.Add (AIKeys.mwu, new SuperKey ( "msu" ));
        }

        protected override void BeginStep()
        {
            BeginSlash(ComboId);
        }

        void BeginSlash ( int id )
        {
            ms.PlayState (0, m_sword_user.SlashKeys[id], 0.1f, EndSlash, null, Slash);
        }

        void Slash ()
        {
            p_slash_attack.Fire ( new SuperKey ( msu.Weapon.SlashName ), msu.Weapon, ms.EventPointsOfState ( m_sword_user.SlashKeys[ComboId] ) [1] - ms.EventPointsOfState ( m_sword_user.SlashKeys[ComboId] ) [0] );
        }

        void EndSlash ()
        {
            AppendStop();
        }

        // AI keys
        public static readonly SuperKey slash_count = new SuperKey ("slash_count");
        public static readonly SuperKey slash_last_id = new SuperKey ( "slash_last_id" );
    }
}