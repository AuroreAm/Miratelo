using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    [path ("player controller")]
    public class player_dash : action
    {
        [link]
        warrior warrior;

        [link]
        skin skin;

        [link]
        stand stand;

        [link]
        motor motor;

        [link]
        dash dash;

        [link]
        backflip backflip;

        [link]
        move move;


        protected override void _step()
        {
            if ( player.dash.down )
            {
                direction direction = direction.forward;

                if ( warrior.target )
                {
                    Vector3 input;
                    input = player.move.normalized;
                    
                    if ( input.sqrMagnitude == 0 )
                    input = Vector3.forward;

                    // rotate input from camera view to world
                    input = vecteur.ldir (camera.o.tps_roty.y, input);

                    // get closest direction
                    float desired_dash_roty = vecteur.rot_direction_y ( Vector3.zero, input );
                    float dash_roty_relative = Mathf.DeltaAngle ( desired_dash_roty , stand.anchor );

                    if ( dash_roty_relative >= -180 )
                    direction = direction.back;
                    if ( dash_roty_relative > -135 )
                    direction = direction.right;
                    if ( dash_roty_relative > -45 )
                    direction = direction.forward;
                    if ( dash_roty_relative > 45 )
                    direction = direction.left;
                    if ( dash_roty_relative > 135 )
                    direction = direction.back;

                    // snap roty
                    float roty = 0;
                    switch (direction)
                    {
                        case direction.back:
                        roty = desired_dash_roty + 180;
                        break;
                        case direction.left:
                        roty = desired_dash_roty + 90;
                        break;
                        case direction.right:
                        roty = desired_dash_roty - 90;
                        break;
                        case direction.forward:
                        roty = desired_dash_roty;
                        break;
                    }
                    stand.roty = roty;
                    skin.roty = roty;
                }

                spam (direction);
            }
        }

        void spam ( direction direction )
        {
            if ( direction != direction.back )
            {
                motor.start_act ( dash._ ( direction ) );
            }
            else
                motor.start_act ( backflip );
        }
    }
}