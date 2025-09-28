using Lyra;
using UnityEngine;

namespace Triheroes.Code.Mecha {
    [path ("mecha")]
    public class aim : acting.second {
        [link]
        mecha_buster.aim aim_buster;
        protected override act get_act() => aim_buster;
    }

    [path ("mecha")]
    public class try_aim_target : action {

        [link]
        mecha_buster.aim aim;
        [link]
        warrior warrior;
        [link]
        mecha_buster buster;

        character target => warrior.target.c;

        protected override void _step()
        {
            aim.at(vecteur.rot_direction_y(buster.position, target.position));
        }
    }

    [path ("mecha")]
    public class charge : action {
        [link]
        mecha_buster.charger charger;

        protected override void _start() {
            link (charger);
        }

        protected override void _step() {
            if (charger.done)
                stop ();
        }
    }

    [path ("mecha")]
    public class wait_for_charge : action {
        [link]
        mecha_buster.charger charger;

        protected override void _step() {
            if (charger.charged) stop ();
        }
    }

    [path ("mecha")]
    public class burst : action {
        [link]
        mecha_buster.charger charger;

        [export]
        public float interval = .25f;
        float next_fire;

        protected override void _start()
        {
            next_fire = 0;
        }

        protected override void _step()
        {
            if ( Time.time > next_fire )
            {
                charger.shoot ();
                next_fire = Time.time + interval;
            }

            if ( charger.done )
                stop ();
        }
    }
}