using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path("player controller")]
    public class player_target : action
    {
        [link]
        warrior warrior;
        [export]
        public float distance = 20;

        protected override void _step()
        {
            if ( player._lock.down && !warrior.target )
            {
                warrior.unlock_target ();
                warrior.lock_target ( warrior.get_nearest_foe (distance) );
                return;
            }

            if ( player._lock.down && warrior.target )
                warrior.unlock_target ();
        }
    }
}
