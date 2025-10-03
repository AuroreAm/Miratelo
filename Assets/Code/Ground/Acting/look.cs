using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    [path ("acting")]
    public abstract class look : action {
        [link]
        skin skin;

        [link]
        protected character c;

        [link]
        stand stand;

        [export]
        public float _angular_speed = 320;

        protected sealed override void _start() {
            stand.use ( this );
        }

        protected sealed override void _step() {
            var rot = get_rot_y ();
            stand.roty = Mathf.MoveTowardsAngle(skin.roty, rot, Time.deltaTime * _angular_speed);

            stand.rotate_skin ();
            
            if ( rot == skin.roty || !stand.active )
            stop ();
        }

        protected abstract float get_rot_y ();
    }
}