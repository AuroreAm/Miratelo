using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using UnityEngine.AI;

// INPROGRESS
/*
namespace Triheroes.Code
{
    public class way_to_target_navi : waypointer2d
    {
        [Depend]
        m_actor ma;
        NavMeshPath path = new NavMeshPath();
        protected override void OnAquire()
        {
            character.StartCoroutine ( LowUpdate () );
        }

        public override void Main()
        {}

        protected override void OnFree()
        {
            character.StopCoroutine ( LowUpdate() );
        }

        IEnumerator LowUpdate ()
        {
            var y = new WaitForSeconds (.9f);

            while (true)
            {
                if (!ma.target) yield return y;

                if (NavMesh.CalculatePath ( dd.position, ma.target.md.position, NavMesh.AllAreas, path ))
                    SetWayPoints ( path.corners );

                yield return y;
            }
        }
    }

    // set character way to actor target
    public class way_to_target : waypointer2d
    {
        [Depend]
        m_actor ma;

        protected override void OnAquire()
        {
            if (!ma.target) return;
            SetWayPoints ( new Vector3[] { dd.position, ma.target.md.position } );
        }

        public override void Main()
        {
            if (!ma.target) return;
            Waypoints [1] = ma.target.md.position;
        }
    }

    public class way_around_target : waypointer2d
    {
        float AngleAmount = 40;
        float Distance;
        int WaypointsCount;
        float InitalRotY;
        
        [Depend]
        m_actor ma;

        public void Set ( float AngleAmount, float distance )
        {
            this.AngleAmount = AngleAmount;
            Distance = distance;
        }

        protected override void OnAquire()
        {
            if (!ma.target) return;

            WaypointsCount = 1 + (int) AngleAmount / 10;
            Waypoints.AddRange ( new Vector3 [WaypointsCount] );
            InitalRotY = Vecteur.RotDirection ( ma.target.md.position, dd.position ).y;
        }

        public override void Main()
        {
            if (!ma.target) return;

            ModifyCircleWay ();
        }

        void ModifyCircleWay ()
        {
            for (int i = 0; i < WaypointsCount - 1; i++)
            {
                Waypoints [i] = ma.target.md.position + Vecteur.LDir ( new Vector3(0,InitalRotY + i * 10,0), Vector3.forward * Distance );
            }
            Waypoints [WaypointsCount - 1] = ma.target.md.position + Vecteur.LDir ( new Vector3(0,InitalRotY + AngleAmount,0), Vector3.forward * Distance );
        }
    }
}
*/