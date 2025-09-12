using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.CapsuleAct;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    [path("mecha ai")]
    public class ai_aim_target : action
    {
        [link]
        motor motor;
        [link]
        aim aim;
        [link]
        warrior warrior;
        [link]
        mecha_buster buster;
        [link]
        move move;
        
        dimension target => ( warrior.target ).system.get <dimension> ();

        protected override void _step()
        {
            if (motor.act == null)
            motor.start_act ( move );

            if (!aim.on)
                motor.start_act2nd ( aim );

            if ( !warrior.target)
            {
                stop ();
                return;
            }

            aim.at ( vecteur.rot_direction_y ( buster.position, target.position ) );
        }
    }

    public class aim : act
    {
        public override int priority => level.sub;

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
