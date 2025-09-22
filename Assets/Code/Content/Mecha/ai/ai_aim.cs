using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path("mecha ai")]
    public class ai_aim : action, acting
    {
        [link]
        mecha_buster.aim aim;

        [link]
        motor motor;

        public void _act_end(act a, bool replaced)
        {}

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
    }

    [path("mecha ai")]
    public class ai_aim_target : action, acting
    {
        [link]
        motor motor;
        [link]
        mecha_buster.aim aim;
        [link]
        warrior warrior;
        [link]
        mecha_buster buster;

        dimension target => warrior.target.get_dimension();

        public void _act_end(act a, bool replaced)
        { }

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