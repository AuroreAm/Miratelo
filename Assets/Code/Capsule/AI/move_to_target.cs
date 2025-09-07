using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.CapsuleAct;
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
        dimension dimension;
        [link]
        move_point point;
        
        Coroutine routine_low_step;
        dimension target => ( warrior.target ).system.get <dimension> ();
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
            return Vector3.Distance ( dimension.position, target.position ) < _stop_distance + dimension.r + target.r;
        }

        protected override void _step()
        {
            if ( close_enough () )
            {
                stop ();
                return;
            }

            if ( !warrior.target )
            stop ();
        }

        void _low_step ()
        {
            if (NavMesh.CalculatePath ( dimension.position, target.position, NavMesh.AllAreas, path ))
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