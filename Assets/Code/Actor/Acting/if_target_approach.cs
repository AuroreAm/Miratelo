using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("actor")]
    public class if_target_approach : action {

        [link]
        warrior warrior;

        float initial_distance;

        protected override void _start() {
            initial_distance = Vector3.Distance ( warrior.c.position.xz (), warrior.target.c.position.xz () );
        }

        protected override void _step() {
            if ( Vector3.Distance ( warrior.c.position.xz (), warrior.target.c.position.xz () ) < initial_distance )
            stop ();
        }
    }
}