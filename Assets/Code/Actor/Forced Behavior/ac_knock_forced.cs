using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class r_knock_forced_ccc : reflection, IElementListener
    {
        [Depend]
        m_element me;

        [Depend]
        ac_knock_forced_ccc akfc;

        bool ForcedThisFrame;

        protected override void OnAquire()
        {
            me.LinkMessage (this);
        }

        public override void Main()
        {
            if ( mst.state == akfc && !ForcedThisFrame )
            akfc.Release ();

            ForcedThisFrame = false;
        }

        protected override void OnFree()
        {
            me.UnlinkMessage (this);
        }

        public void OnMessage(int message)
        {
            if ( message == MessageKey.knock_forced )
            {
                mst.SetState ( akfc, Pri.ForcedAction );
                ForcedThisFrame = true;
            }
        }
    }

    public class ac_knock_forced_ccc : action
    {
        [Depend]
        m_capsule_character_controller mccc;
        
        [Depend]
        m_last_knock mlk;

        delta_curve cu;

        bool UsedGravity;

        public override void Create()
        {
            cu = new delta_curve ( SubResources <CurveRes>.q ( new SuperKey ("jump") ).Curve );
        }

        protected override void BeginStep()
        {
            mccc.Aquire (this);
            cu.Start ( mlk.LastKnockDir.y, .5f );
        }

        public void Release ()
        {
            if (on)
            AppendStop ();
        }

        protected override void Stop()
        {
            mccc.Free (this);
        }

        protected override bool Step()
        {
            mccc.dir += new Vector3 ( 0, cu.TickDelta (), 0 );
            return false;
        }
    }
}