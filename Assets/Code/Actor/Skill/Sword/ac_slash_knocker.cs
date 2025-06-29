using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class ac_slash_knocker : action
    {
        [Depend]
        m_sword_user msu;
        [Depend]
        m_skin ms;

        int ComboId = 0;

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
            p_slash_attack.Fire ( new SuperKey ( msu.Weapon.SlashName ), msu.Weapon, ms.EventPointsOfState ( m_sword_user.SlashKeys[ComboId] ) [1] - ms.EventPointsOfState ( m_sword_user.SlashKeys[ComboId] ) [0], Knock );
        }

        void Knock (int Hitted)
        {
            Element.Clash ( msu.Weapon.element, Hitted, new Knock ( Vecteur.LDir (ms.rotY, Vector3.forward) * 20, 10 ) );
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
