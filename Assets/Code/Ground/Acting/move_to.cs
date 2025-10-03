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

        Coroutine routine_low_step;
        NavMeshPath path = new NavMeshPath ();

        protected sealed override void _start()
        {
            link ( point );
            routine_low_step = phoenix.o.StartCoroutine ( ie_low_step () );
            __start ();
        }

        protected virtual void __start () {}

        protected abstract Vector3 get_target (); 
        protected virtual float get_offset_stop_distance () => 0;

        protected bool close_enough ()
        {
            return Vector3.Distance ( capsule.c.position, get_target() ) < _stop_distance + get_offset_stop_distance ();
        }

        protected sealed override void _stop()
        {
            point.clear ();
            unlink ( point );
            phoenix.o.StopCoroutine ( routine_low_step );
        }

        IEnumerator ie_low_step ()
        {
            var y = new WaitForSeconds (.75f);
            while (true)
            {
                _low_step ();
                yield return y;
            }
        }

        void _low_step ()
        {
            if (NavMesh.CalculatePath ( capsule.c.position, get_target () , NavMesh.AllAreas, path ))
                point.set_way ( path.corners );
        }
    }
}