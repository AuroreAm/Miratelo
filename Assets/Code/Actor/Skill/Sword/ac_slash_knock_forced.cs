using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{

    public class ac_active_knock_forcer : action
    {
        [Depend]
        m_knock_forcer mkf;
        protected override void BeginStep()
        {
            mkf.Aquire (this);
        }

        protected override void Stop()
        {
            mkf.Free (this);
        }
    }

    public class m_knock_forcer : controller
    {
        [Depend]
        m_sword_user msu;
        [Depend]
        m_capsule_character_controller mccc;
        bool UsedGravity;
        int Target;

        delta_curve cu;

        public override void Create()
        {
            cu = new delta_curve ( SubResources <CurveRes>.q ( new SuperKey ("jump") ).Curve );
        }

        protected override void OnAquire()
        {
            mccc.Aquire (this);
            Target = -1;
        }

        public override void Main()
        {
            mccc.dir += new Vector3 ( 0, cu.TickDelta (), 0 );

            if (Target != -1)
            Element.Clash ( msu.Weapon.element, Target, new Knock ( Vector3.up * 2) );
        }

        public void SetTarget (int Hitted)
        {
            Target = Hitted;
            cu.Start ( 2, .5f );
        }

        protected override void OnFree()
        {
            mccc.Free (this);
            Target = -1;
        }
    }

    [Unique]
    public class ac_slash_knocked_forced : action
    {
        [Depend]
        m_sword_user msu;
        [Depend]
        m_skin ms;
        [Depend]
        m_knock_forcer mkf;

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
            mkf.SetTarget (Hitted);
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
