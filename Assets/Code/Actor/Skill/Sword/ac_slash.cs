using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_slash : action
    {
        [Depend]
        m_sword_user msu;
        [Depend]
        m_skin ms;

        [Export]
        public int ComboId = 0;

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
    }
}