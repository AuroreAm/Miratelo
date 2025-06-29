using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public struct Hook
    {
        public Vector3 dir;
        public float duration;

        public Hook ( Vector3 Dir, float Duration )
        {
            dir = Dir;
            duration = Duration;
        }
    }

    public class r_hooked_up_ccc : reflection, IElementListener <Hook>
    {
        [Depend]
        m_element me;

        [Depend]
        ac_hooked_up_ccc ahuc;

        bool ForcedThisFrame;

        protected override void OnAquire()
        {
            me.LinkMessage (this);
        }

        public override void Main()
        {
            if ( mst.state == ahuc && !ForcedThisFrame )
            ahuc.Release ();

            ForcedThisFrame = false;
        }

        protected override void OnFree()
        {
            me.UnlinkMessage (this);
        }

        public void OnMessage(int message, Hook context)
        {
            if ( message == MessageKey.hooked_up )
            {
                ahuc.hookDir = context.dir;
                ahuc.hookDuration = context.duration;
                mst.SetState ( ahuc, Pri.ForcedAction );

                ForcedThisFrame = true;
            }
        }
    }

    public class ac_hooked_up_ccc : action
    {
        [Depend]
        m_capsule_character_controller mccc;

        public Vector3 hookDir;
        public float hookDuration;

        delta_curve cu;

        public override void Create()
        {
            cu = new delta_curve ( SubResources <CurveRes>.q ( new SuperKey ("jump") ).Curve );
        }

        protected override void BeginStep()
        {
            mccc.Aquire (this);
            cu.Start ( hookDir.y, hookDuration );
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