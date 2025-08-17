using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;
using UnityEngine.AI;

namespace Triheroes.Code
{
    public class ar_way_to_target : reflexion
    {
        [Depend]
        d_actor da;
        [Depend]
        s_mind sm;
        [Depend]
        d_dimension dd;
        [Depend]
        ar_move_way_point amwp;
        [Depend]
        t_move_to_target tmtt;

        NavMeshPath path = new NavMeshPath ();
        Coroutine RoutineLowUpdate;

        protected override void Reflex()
        {
            if ( da.target && tmtt.on )
                Stage.Start (this);
        }

        protected override void Start()
        {
            RoutineLowUpdate = Stage.o.StartCoroutine ( LowUpdateRoutineIENumerator () );
        }

        protected override void Step()
        {
            if ( da.target && tmtt.on && tmtt.CloseEnoughToTarget ()  )
            {
                amwp.Clear ();
                tmtt.Finish ();
                SelfStop ();
                return;
            }
            
            if ( !da.target && !tmtt.on )
            {
                amwp.Clear ();
                SelfStop ();
            }
        }

        protected override void Stop()
        {
            Stage.o.StopCoroutine ( RoutineLowUpdate );
        }

        void LowUpdate ()
        {
            if (NavMesh.CalculatePath ( dd.position, da.target.dd.position, NavMesh.AllAreas, path ))
                amwp.SetWayPoints ( path.corners );

            return;
        }

        IEnumerator LowUpdateRoutineIENumerator ()
        {
            var y = new WaitForSeconds (.75f);
            while (true)
            {
                LowUpdate ();
                yield return y;
            }
        }
    }
}