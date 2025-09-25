using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;
using UnityEngine.AI;

namespace Triheroes.Code
{
    [path ("AI")]
    public class move_to_target : action
    {
        [export]
        public float _stop_distance;

        [link]
        warrior warrior;
        [link]
        capsule capsule;
        [link]
        move_point point;
        
        Coroutine routine_low_step;
        capsule target => warrior.target.c.system.get <capsule> ();
        NavMeshPath path = new NavMeshPath ();

        protected override void _start()
        {
            link ( point );
            routine_low_step = phoenix.o.StartCoroutine ( ie_low_step () );
        }

        protected override void _stop()
        {
            point.clear ();
            unlink ( point );
            phoenix.o.StopCoroutine ( routine_low_step );
        }

        bool close_enough ()
        {
            if ( !warrior.target ) return false;
            return Vector3.Distance ( capsule.c.position, target.c.position ) < _stop_distance + capsule.r + target.r;
        }

        protected override void _step()
        {
            if ( !warrior.target )
            stop ();
        }

        void _low_step ()
        {
            if (NavMesh.CalculatePath ( capsule.c.position, target.c.position, NavMesh.AllAreas, path ))
                point.set_way ( path.corners );
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
    }
}