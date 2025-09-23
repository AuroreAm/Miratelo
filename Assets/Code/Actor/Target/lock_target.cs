using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class lock_target : controller
    {
        [link]
        warrior warrior;

        [link]
        stand stand;

        [link]
        move move;

        protected override void _step()
        {
            if ( move.on && warrior.target != null )
            if ( move.state == animation.idle || move.state == animation.brake || move.state == animation.rotation_brake )
            stand.roty = vecteur.rot_direction_y ( ((actor)warrior).position,((actor)warrior.target).position );
        }
    }
}
