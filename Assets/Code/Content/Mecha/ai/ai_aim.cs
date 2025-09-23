using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path("mecha ai")]
    public class ai_aim : action, act_handler
    {
        [link]
        mecha_buster.aim aim;

        [link]
        motor motor;

        protected override void _start()
        {
            motor.start_act2nd(aim, this);
        }

        protected override void _stop()
        {
            if (aim.on)
            motor.stop_act2nd (this);
        }

        protected override void _step()
        {
            if (!aim.on)
                stop();
        }

        public void _act_end(act a, act_status status) {
        }
    }

    [path("mecha ai")]
    public class ai_aim_target : action, act_handler
    {
        [link]
        motor motor;
        [link]
        mecha_buster.aim aim;
        [link]
        warrior warrior;
        [link]
        mecha_buster buster;

        character target => warrior.target.c;

        public void _act_end(act a, act_status status) {
        }

        protected override void _step()
        {
            if (!warrior.target)
            {
                stop();
                return;
            }

            aim.at(vecteur.rot_direction_y(buster.position, target.position));
        }
    }
}