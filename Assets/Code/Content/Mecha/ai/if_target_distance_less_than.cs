using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    [path("mecha ai")]
    public class if_target_distance_less_than : action
    {
        [link]
        warrior warrior;

        [export]
        public float distance;

        character target => warrior.target.c;

        protected override void _step()
        {
            Debug.Log ($"{Vector3.Distance(warrior.c.position.xz (), target.position.xz () )} < {distance}");
            if ( warrior.target && Vector3.Distance(warrior.c.position.xz (), target.position.xz () ) < distance )
                stop ();
        }
    }
}