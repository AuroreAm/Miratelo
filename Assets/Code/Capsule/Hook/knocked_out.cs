using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code.CapsuleAct
{
    /*
    public class react_knock : moon, ruby<knock>
    {
        [link]
        knocked_out_fall knocked;
        
        [link]
        motor motor;

        public void _radiate(knock gleam)
        {
            knocked.set ( gleam.dir, gleam.speed );
            motor.start_act ( knocked );
        }
    }

    [need_ready]
    public class knocked_out_fall : act
    {
        public override priority priority => priority.reaction;

        [link]
        capsule capsule;
        [link]
        capsule.gravity gravity;
        [link]
        ground ground;

        [link]
        skin skin;
        [link]
        motor motor;

        delta_curve cu;

        [link]
        knocked_out_on_ground on_ground;
        
        Vector3 direction;
        Vector3 dir;
        float speed;
        
        bool done;

        protected override void _ready ()
        {
            cu = new delta_curve ( triheroes_res.curve.q ( animation.jump ).curve );
        }

        public void set ( Vector3 _dir, float _speed )
        {
            dir = _dir;
            speed = _speed;
            ready_for_tick ();
        }

        protected override void _start()
        {
            link ( capsule );
            link ( gravity );

            skin.play ( new skin.animation ( animation.hit_knocked_a, this ) {end = animation_done} );

            direction = dir.normalized;
            cu.start ( dir.magnitude, dir.magnitude / speed );

            done = false;
        }

        void animation_done ()
        {
            done = true;
        }

        protected override void _step()
        {
            capsule.dir = direction * cu.tick_delta ();

            if ( done && ground.raw )
            {
                stop ();
                motor.start_act ( on_ground );
            }
        }
    }*/

    /*public class knocked_out_on_ground : act
    {
        [link]
        capsule capsule;
        [link]
        capsule.gravity gravity;
        [link]
        ground ground;

        [link]
        skin skin;
        [link]
        motor motor;
        [link]
        knocked_stand_up stand_up;

        public override priority priority => priority.reaction;
        bool done;

        protected override void _start()
        {
            link ( capsule );
            link ( gravity );

            done = false;
            skin.play ( new skin.animation ( animation.hit_knocked_b, this ) { end = animation_done } );
        }

        void animation_done ()
        {
            done = true;
        }

        protected override void _step()
        {
            if ( done && ground.raw && capsule.dir.sqrMagnitude == 0 )
            {
                stop ();
                motor.start_act ( stand_up );
            }
        }
    }

    public class knocked_stand_up : act
    {
        public override priority priority => priority.action;
        
        [link]
        capsule capsule;
        [link]
        capsule.gravity gravity;
        [link]
        skin skin;

        protected override void _start()
        {
            link ( capsule );
            link ( gravity );
            skin.play ( new skin.animation ( animation.stand_up, this ) { end = stop } );
        }
    }*/
}
