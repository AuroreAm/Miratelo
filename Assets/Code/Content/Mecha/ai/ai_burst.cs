using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
/*
namespace Triheroes.Code.Mecha
{
    [path("mecha ai")]
    public class ai_burst : action
    {
        [export]
        public float interval = .25f;

        [link]
        mecha_buster buster;

        [link]
        mecha_buster.aim aim;

        float next_fire;

        protected override void _start()
        {
            next_fire = 0;
        }

        protected override void _step()
        {
            if ( ! buster.can_shot )
            {
                stop ();
                return;
            }

            if ( Time.time > next_fire )
            {
                aim.shot ();
                next_fire = Time.time + interval;
            }
        }
    }
}
*/