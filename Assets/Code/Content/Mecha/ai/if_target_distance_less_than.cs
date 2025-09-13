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

        dimension target => warrior.target.get_dimension ();

        protected override void _step()
        {
            if (warrior.target && Vector3.Distance(warrior.get_dimension().position, target.position) < distance)
                stop ();
        }
    }
}