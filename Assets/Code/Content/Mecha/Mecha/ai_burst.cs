using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    [path("mecha ai")]
    public class ai_burst : action
    {
        [export]
        public float interval = .2f;

        [link]
        mecha_buster.charger charger;

        float next_fire;

        protected override void _start()
        {
            next_fire = 0;
        }

        protected override void _step()
        {
            if ( ! charger.can_shot )
            {
                stop ();
                return;
            }

            if ( Time.time > next_fire )
            {
                charger.shot ();
                next_fire = Time.time + interval;
            }
        }
    }
}
