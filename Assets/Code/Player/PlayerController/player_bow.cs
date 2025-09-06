using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("player controller")]
    public class player_bow : action2, acting
    {
        [link]
        player_lateral lateral;

        [link]
        equip equip;

        [link]
        motor motor;

        [link]
        aim aim;

        public void _act_end(act m)
        {}

        protected override void _stepa()
        {
            if ( equip.weapon_user is bow_user && player.aim )
            swap ();
        }

        protected override void _startb()
        {
            motor.start_act2nd ( aim, this );
            this.link (lateral);
        }

        protected override void _stepb()
        {
            aim.at ( camera.o.tps.rotx, camera.o.tps.roty );

            if ( !aim.on )
            {
                swap ();
                return;
            }
            
            if ( player.aim.up )
            {
                motor.stop_act2nd (this);
                swap ();
            }

            if ( player.action2.down )
            aim.shot ();
        }

        protected override void _stopb()
        {
            this.unlink ( lateral );
        }
    }
}