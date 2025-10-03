using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("acting")]
    public class look_target : look
    {
        [link]
        warrior warrior;
        
        character target => warrior.target.c;

        protected override float get_rot_y() {
            return vecteur.rot_direction_y ( c.position,target.position );
        }
    }
}
