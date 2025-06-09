using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("actor")]
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
            if (msu.state == StateKey.zero)
            BeginSlash(ComboId);
            else
            EndSlash();
        }

        void BeginSlash ( int id )
        {
            if (!msu.on)
            Debug.LogError("the character is not sword ready");

            msu.state = StateKey.slash;
            ms.PlayState (0, m_sword_user.SlashKeys[id], 0.1f, EndSlash, null, Slash);
        }

        void Slash ()
        {
            p_slash_attack.s_slash_attack.Fire( msu.Weapon, ms.EventPointsOfState ( m_sword_user.SlashKeys[ComboId] ) [1] - ms.EventPointsOfState ( m_sword_user.SlashKeys[ComboId] ) [0] );
        }

        void EndSlash ()
        {
            AppendStop();
        }

        protected override void Stop()
        {
            if (msu.state == StateKey.slash)
            msu.state = StateKey.zero;
        }
    }
}