using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{

    public class ac_active_hooker_up : action
    {
        [Depend]
        m_hooker_up mhu;
        protected override void BeginStep()
        {
            mhu.Aquire (this);
        }

        protected override void Stop()
        {
            mhu.Free (this);
        }
    }

    public class m_hooker_up : controller
    {
        [Depend]
        m_sword_user msu;
        [Depend]
        m_capsule_character_controller mccc;
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
            Element.SendMessage ( Target, MessageKey.hooked_up, new Hook ( Vector3.up * 2, .5f ) );
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

    public class ac_slash_hooker_up : action
    {
        [Depend]
        m_sword_user msu;
        [Depend]
        m_skin ms;
        [Depend]
        m_hooker_up mhu;

        int ComboId = 0;

        protected override void BeginStep()
        {
            BeginSlash(ComboId);
        }

        void BeginSlash ( int id )
        {
            if (!msu.on)
            Debug.LogError("the character is not sword ready");

            ms.PlayState (0, m_sword_user.SlashKeys[id], 0.1f, EndSlash, null, Slash);
        }

        void Slash ()
        {
            p_slash_attack.Fire ( new SuperKey ( msu.Weapon.SlashName ), msu.Weapon, ms.EventPointsOfState ( m_sword_user.SlashKeys[ComboId] ) [1] - ms.EventPointsOfState ( m_sword_user.SlashKeys[ComboId] ) [0], Hook );
        }

        void Hook (int Hitted)
        {
            mhu.SetTarget (Hitted);
        }

        void EndSlash ()
        {
            AppendStop();
        }
    }
}
