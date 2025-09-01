using System.Collections;
using System.Collections.Generic;
using Lyra.Spirit;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ar_move_way_point : reflexion, IMotorHandler
    {
        [Depend]
        ac_ground_complex agc;
        [Depend]
        s_motor sm;
        [Depend]
        d_dimension dd;

        List <Vector3> Points = new List<Vector3> ();
        public float speed = 7;

        public int Count => Points.Count;
        public void SetPoint (int i, Vector3 point)
        {
            Points [i] = point;
        }

        public void OnMotorEnd(motor m) {}

        protected override void Reflex()
        {
            if (Points.Count != 0)
            {
                sm.SetState ( agc, this );
                if ( agc.on )
                Stage.Start (this);
            }
        }
        
        protected override void Stop()
        {
            lastDir = Vector3.zero;
        }

        /// <summary>
        /// last direction the character moved
        /// </summary>
        public Vector3 LastDir => lastDir;
        Vector3 lastDir;
        
        protected override void Step()
        {
            if (Points.Count == 0 || !agc.on )
            {
                SelfStop ();
                return;
            }

            Vector3 direction = ( Points [0].Flat () - dd.position.Flat () ).normalized;
            direction = direction * speed;
            agc.Walk ( direction );
            lastDir = direction * Time.deltaTime;

            while ( Vector3.Distance ( dd.position.Flat (), Points  [0].Flat () ) < lastDir.magnitude + .5f )
            {
                Points.RemoveAt (0);
                if (Points.Count == 0)
                break;
            }
        }

        public void SetWayPoints ( Vector3 [] points )
        {
            Points.Clear ();
            Points.AddRange ( points );
        }
        public void Clear ()
        {
            Points.Clear ();
        }
    }
}