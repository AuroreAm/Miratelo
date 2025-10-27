using Lyra;
using UnityEngine;
using UnityEngine.AI;
using Triheroes.Code.Axeal;
using System.Collections;

namespace Triheroes.Code {
    [path ("acting")]
    public abstract class move_to : action {
        [export]
        public float _stop_distance;

        [link]
        capsule capsule;

        [link]
        move_point point;
        NavMeshPath path = new NavMeshPath ();

        interval interval;
        public move_to () {
            interval = new interval (_low_step,.75f);
        }

        protected sealed override void _start()
        {
            link ( point );
        }

        protected abstract Vector3 get_target (); 
        protected virtual float get_offset_stop_distance () => 0;

        protected bool close_enough ()
        {
            return Vector3.Distance ( capsule.c.position, get_target() ) < _stop_distance + get_offset_stop_distance ();
        }

        protected override void _step() {
            interval.tick ( Time.deltaTime );
        }

        protected sealed override void _stop()
        {
            point.clear ();
            unlink ( point );
        }

        void _low_step ()
        {
            if (NavMesh.CalculatePath ( capsule.c.position, get_target () , NavMesh.AllAreas, path ))
                point.set_way ( path.corners );
        }
    }
}