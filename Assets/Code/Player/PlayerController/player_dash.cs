using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triheroes.Code.CapsuleAct;
using Lyra;

namespace Triheroes.Code
{
    [path ("player controller")]
    public class player_dash : action, acting
    {
        [link]
        warrior warrior;

        [link]
        skin skin;

        [link]
        motor motor;

        [link]
        dash dash;

        [link]
        backflip backflip;


        protected override void _step()
        {
            if ( player.dash.down )
            {
                direction direction = direction.forward;

                if (warrior.target)
                {
                    Vector3 input;
                    input = player.move.normalized;
                    input = vecteur.ldir(Mathf.DeltaAngle(skin.roty, camera.o.tps.roty) * Vector3.up, input);

                    if (Mathf.Abs(input.x) > Mathf.Abs(input.z))
                    {
                        if (input.x < 0)
                            direction = direction.left;
                        else
                            direction = direction.right;
                    }
                    else if (input.z < 0)
                        direction = direction.back;
                }
                spam (direction);
            }
        }

        public void _act_end(act m) {}

        void spam ( direction direction )
        {
            if ( direction != direction.back )
            {
                dash.set ( direction );
                motor.start_act ( dash, this );
            }
            else
                motor.start_act ( backflip, this );
        }

    }
}
