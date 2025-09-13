using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class aim : act
    {
        public override priority priority => priority.sub;

        // target aim rotation
        float roty, rotx;
        float speed => 720 * Time.deltaTime;

        [link]
        bow_user bow_user;
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
            at (0,skin.roty);
        }

        public void at (float x, float y)
        {
            roty = y;
            rotx = x;
        }

        protected override void _step ()
        {
            float target_y = Mathf.DeltaAngle ( bow_user.weapon.rot.y, skin.roty_direct ) + roty;
            stand.roty = Mathf.MoveTowardsAngle (stand.roty,target_y, speed );
            skin.ani.SetFloat ( hash.x, Mathf.DeltaAngle ( 0, rotx ) );
        }
        
        public void shot ()
        {
            arrow.fire ( bow_user.weapon.arrow, bow_user.weapon.string_position, Quaternion.Euler ( bow_user.weapon.rot ), 30 );
        }

        protected override void _stop ()
        {
            skin.stop (skin.upper);
        }
    }
}
