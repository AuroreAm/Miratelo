using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Unique]
    public class ai_move_way : action
    {
        [Depend]
        m_dimension md;
        [Depend]
        ac_ground_complex agc;
        [Depend]
        mc_motor mm;

        waypointer2d way;

        public float speed = 7;

        public void SetWayPointer ( waypointer2d way )
        {
            way.ptr = 0;
            this.way = way;
            way.Aquire ( this );
        }

        protected override void BeginStep()
        {
            if (way == null)
            Debug.LogError ("No way pointer set before ai move");
            LastDir = Vector3.zero;
        }

        protected override void Stop()
        {
            way = null;
        }

        Vector3 LastDir;
        protected override bool Step()
        {
            if (way.WayPointCount == 0) return false;

            if ( mm.state == null )
            mm.SetState ( agc );

            if (mm.state != agc) return false;

            while ( Vector3.Distance (md.position.Flat(), way.GetPoint (way.ptr)) < LastDir.magnitude + .5f )
            {
                way.ptr ++;

                if (way.ptr >= way.WayPointCount )
                return true;
            }

            Vector3 direction = ( way.GetPoint (way.ptr) - md.position.Flat() ).normalized;
            direction = direction * speed;
            agc.Walk ( direction );
            LastDir = direction * Time.deltaTime;

            return false;
        }
    }

    public abstract class waypointer2d : controller
    {
        [Depend]
        protected m_dimension md;
        
        /// <summary>
        /// get a point in the waypoints flat in 2d
        /// </summary>
        public Vector3 GetPoint (int i) => new Vector3 ( Waypoints [i].x, 0, Waypoints [i].z );
        public int WayPointCount => Waypoints.Count;
        protected List <Vector3> Waypoints = new List<Vector3> ();
        public int ptr;

        #if UNITY_EDITOR
        public sealed override void Create()
        {
            md.character.OnDrawGizmos += Debug;
        }

        protected void SetWayPoints ( Vector3 [] WayPoints )
        {
            ptr = 0;
            Waypoints.Clear ();
            Waypoints.AddRange ( WayPoints );
        }

        void Debug ()
        {
            if (!on) return;

            for (int i = 0; i < Waypoints.Count; i++)
                Gizmos.DrawWireSphere ( Waypoints [i], .1f );
            for (int i = 0; i < Waypoints.Count - 1; i++)
                Gizmos.DrawLine ( Waypoints[i], Waypoints [i+1] );
        }
        #endif
    }
}