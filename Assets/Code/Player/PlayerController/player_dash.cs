using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using Triheroes.Code.Axeal;

namespace Triheroes.Code
{
    [path ("player controller")]
    public class player_decoherence_blink : action
    {
        [link]
        warrior warrior;

        [link]
        skin skin;

        [link]
        stand stand; 

        [link]
        ground ground;

        [link]
        player_jump jump;

        [link]
        skills s;

        buffer iground = new buffer ( .1f );
        buffer idash = new buffer ( .25f );

        decoherence_blink skill => s.get <decoherence_blink> ();

        protected override void _step()
        {
            if ( player.dash.down )
            idash.stack ();

            if ( ground )
            iground.stack ();

            if ( idash.on && iground.on )
            {
                direction direction = direction.forward;

                if ( warrior.target )
                {
                    Vector3 input;
                    input = player.move.normalized;
                    
                    if ( input.sqrMagnitude == 0 )
                    input = Vector3.forward;

                    // rotate input from camera view to world
                    input = vecteur.ldir (tps.main_roty.y, input);

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

                var success = skill.spam (direction);

                if ( success ) {
                    iground.clear ();
                    idash.clear ();
                }
            }

            if ( jump.jump_down () && skill.active() ) {
                skill.prepare_jump ();
                jump.start_jump ();
            }

            if ( iground.on && idash.on && jump.active () ) {
                skill.spam_decoherence_air_only ();
                iground.clear ();
                idash.clear ();
            }
        }
    }
}