using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.CapsuleAct;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    [path("mecha ai")]
    public class ai_aim_target : action, acting
    {
        [link]
        motor motor;
        [link]
        aim aim;
        [link]
        warrior warrior;
        [link]
        mecha_buster buster;
        
        dimension target => warrior.target.get_dimension ();

        public void _act_end(act a, bool replaced)
        {}

        protected override void _step()
        {
            if (!aim.on)
                motor.start_act ( aim, this );

            if ( !warrior.target)
            {
                stop ();
                return;
            }

            aim.at ( vecteur.rot_direction_y ( buster.position, target.position ) );
        }

        protected override void _stop()
        {
            if (aim.on)
            motor.stop_act ( this );
        }
    }

    public class aim : act
    {
        public override priority priority => priority.action;

        // target aim rotation
        float roty;
        float speed => 720 * Time.deltaTime;

        [link]
        mecha_buster buster;
        [link]
        skin skin;
        [link]
        stand stand;

        protected override void _start ()
        {
            begin_aim ();
            stand.use (this);
        }

        void begin_aim ()
        {
            skin.hold ( new skin.animation ( animation.begin_aim, this ) { layer = skin.upper } );
            at ( skin.roty );
        }

        public void at ( float y )
        {
            roty = y;
        }

        protected override void _step ()
        {
            float target_y = Mathf.DeltaAngle ( buster.roty, skin.roty_direct ) + roty;

            stand.roty = Mathf.MoveTowardsAngle (stand.roty,target_y, speed );
            stand.rotate_skin ();
        }
        
        public void shot ()
        {
            arrow.fire ( mecha_buster.mecha_plasma, buster.position, Quaternion.Euler ( buster.rot ), 30 );
        }

        protected override void _stop ()
        {
            skin.stop (skin.upper);
        }
    }
}
