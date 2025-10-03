using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
/*
namespace Triheroes.Code
{
    [path ("mecha ai")]
    public class if_target_approach_rappidly : action
    {
        [link]
        warrior warrior;
        [link]
        character c;
        [link]
        move_point point;
        character target => warrior.target.c;

        float target_distance;

        protected override void _start ()
        {
            if (!warrior.target) return;

            target_distance = Vector3.Distance ( c.position, target.position );
        } 

        protected override void _step ()
        {
            if (!warrior.target) return;

            if ( Vector3.Distance ( c.position, target.position ) < target_distance - point.lastdir.magnitude * 2 )
            stop ();

            target_distance = Vector3.Distance ( c.position, target.position );
        }
    }
}*/
