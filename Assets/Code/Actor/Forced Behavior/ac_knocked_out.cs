using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    //INPROGRESS
    /*
    public class r_knocked_out : reflection, IElementListener <Knock>
    {
        [Depend]
        m_element me;

        action KnockedOut;

        [Depend]
        ac_knocked_out ako;

        public override void Create()
        {
            TreeStart (character);
            new sequence () {repeat = false};
                new ac_knocked_out ();
                new ac_knocked_out_impact ();
                new ac_knocked_out_impact_recovery ();
            end ();
            KnockedOut = TreeFinalize ();
        }

        protected override void OnAquire()
        {
            me.LinkMessage (this);    
        }

        public override void Main()
        {
        }

        protected override void OnFree()
        {
            me.UnlinkMessage (this);
        }

        public void OnMessage (int message, Knock context)
        {
            if (message == MessageKey.knocked_out)
            {
                ako.dir = context.dir;
                ako.speed = context.speed;
                mm.SetState ( KnockedOut, Pri.ForcedAction2nd );
            }
        }
    }

    public class ac_knocked_out : action
    {
        [Depend]
        m_capsule_character_controller mccc;

        [Depend]
        m_gravity_mccc mgm;

        [Depend]
        m_skin ms;

        public Vector3 dir;
        public float speed;

        delta_curve cu;
        
        Vector3 direction;

        public override void Create()
        {
            cu = new delta_curve ( SubResources <CurveRes>.q ( new SuperKey ("jump") ).Curve );
        }

        protected override void BeginStep()
        {
            mccc.Aquire (this);
            mgm.Aquire (this);
            ms.PlayState ( 0, AnimationKey.hit_knocked_a, 0.1f );

            direction = dir.normalized;
            cu.Start ( dir.magnitude, dir.magnitude / speed );
        }

        protected override bool Step()
        {
            mccc.dir += direction * cu.TickDelta ();

            if ( mccc.mgd.onGroundAbs )
            return true;

            return false;
        }

        protected override void Stop()
        {
            mccc.Free (this);
            mgm.Free (this);
        }
    }

    public class ac_knocked_out_impact : action
    {
        [Depend]
        m_skin ms;

        [Depend]
        m_capsule_character_controller mccc;
        [Depend]
        m_gravity_mccc mgm;

        protected override void BeginStep()
        {
            ms.PlayState (0, AnimationKey.hit_knocked_b, 0.1f,AnimationDone);
            mgm.Aquire (this);
            mccc.Aquire (this);
        }

        void AnimationDone ()
        {
            AppendStop ();
        }

        protected override void Stop()
        {
            mgm.Free (this);
            mccc.Free (this);
        }
    }

    public class ac_knocked_out_impact_recovery : action
    {
        [Depend]
        m_skin ms;

        [Depend]
        m_capsule_character_controller mccc;
        [Depend]
        m_gravity_mccc mgm;

        protected override void BeginStep()
        {
            ms.PlayState (0, AnimationKey.stand_up, 0.1f,AnimationDone);
            mgm.Aquire (this);
            mccc.Aquire (this);
        }

        void AnimationDone ()
        {
            AppendStop ();
        }

        protected override void Stop()
        {
            mgm.Free (this);
            mccc.Free (this);
        }
    }*/
}
