using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.CapsuleAct
{
    public class react_hook : moon, ruby <hook>, acting
    {
        [link]
        hooked hooked;
        
        [link]
        motor motor;

        public bool on { get; }

        public void _act_end(act m)
        {}

        public void _radiate(hook gleam)
        {
            hooked.set ( gleam.curve, gleam.dir, gleam.duration );
            motor.start_act (hooked, this);
        }
    }

    public class hooked : act, gold <hook_spam>
    {
        public override int priority => level.reaction;

        [link]
        capsule capsule;

        float time;
        const float timeout = 1;

        delta_curve cu;
        Vector3 dir;

        public void set ( AnimationCurve _curver, Vector3 _dir, float _duration )
        {
            cu = new delta_curve ( _curver );
            dir = _dir;
            cu.start ( _dir.magnitude, _duration );
            time = 0;
        }

        protected override void _start()
        {
            link ( capsule );
            time = 0;
        }

        protected override void _step()
        {
            capsule.dir += dir * cu.tick_delta ();
            time += Time.deltaTime;

            if ( time > timeout )
            stop ();
        }

        public void _radiate(hook_spam gleam)
        {
            time = 0;
        }


        public void _act_end(act m)
        {}
    }

}