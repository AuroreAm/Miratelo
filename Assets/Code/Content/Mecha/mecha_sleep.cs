using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using UnityEngine.SocialPlatforms;

namespace Triheroes.Code {
    public class mecha_sleep : act {
        [link]
        skin skin;

        public const float angular_speed = 360;
        public float target_roty { private set; get; }
        public float roty { private set; get; }

        public override priority priority => priority.action;

        protected override void _start() {
            skin.play(new skin.animation(anim.sleep, this));
            look_roty(roty);
        }

        protected override void _step() {
            roty = Mathf.MoveTowardsAngle(roty, target_roty, angular_speed * Time.deltaTime);
            skin.ani.SetFloat(sh.x, roty / 90);
        }

        public void look_roty(float _roty) {
            _roty = Mathf.DeltaAngle(0, _roty);
            _roty = Mathf.Clamp(_roty, -90, 90);
            target_roty = _roty;
        }
    }

    [path("mecha")]
    public class sleep : acting.first {
        [link]
        mecha_sleep idle;

        protected override act get_act() {
            return idle;
        }
    }

    [path("mecha")]
    public class scan_target : action {
        [link]
        skin skin;
        [link]
        mecha_sleep sleep;
        [link]
        warrior warrior;
        [link]
        temp_target temp;
        [link]
        mecha_eye eye;

        interval interval;
        float t;
        int ptr;

        protected override void _ready() {
            interval = new interval(see_for_target, .1f);
        }

        protected override void _step() {
            if (t <= 0) {
                t = Random.Range(0, 3);
                sleep.look_roty(Random.Range(-90f, 90f));
            }

            interval.tick(Time.deltaTime);
            t -= Time.deltaTime;
        }

        void see_for_target() {
            var foes = xenos.get_foes(warrior.faction);

            if (foes.Count == 0) return;
            if (ptr >= foes.Count) ptr = 0;

            float roty = skin.roty_direct + sleep.roty;
            if (Mathf.Abs(Mathf.DeltaAngle(roty+ sleep.roty, vecteur.rot_direction_y(skin.position, foes[ptr].c.position))) < 60)
                // TODO remove hardocded vector3.up
                if (!Physics.Linecast(eye.position, foes[ptr].c.position + Vector3.up, vecteur.Solid)) {
                    temp.target_interest = foes[ptr];
                    stop ();
                    return;
                }

            ptr++;
        }
    }

    [path("mecha")]
    public class inspect_target : action {

        [export]
        public float _max_distance = 64;
        [export]
        public float _duration = 1;

        [link]
        skin skin;
        [link]
        warrior warrior;
        [link]
        mecha_sleep sleep;

        [link]
        temp_target temp;


        [link]
        mecha_eye eye;

        float max_distance;
        float t;

        protected override void _ready() {
            max_distance = _max_distance;
        }

        protected override void _start() {
            t = _duration;
        }

        protected override void _step() {
            if (!temp.target_interest) {
                stop();
                return;
            }

            if (t <= 0 && Vector3.Distance(temp.target_interest.c.position, warrior.c.position) < max_distance) {
                warrior.lock_target(temp.target_interest);
                temp.target_interest = null;
                stop ();
                return;
            }

            sleep.look_roty( skin.roty - vecteur.rot_direction_y(warrior.c.position, temp.target_interest.c.position));

            // NOTE closer character are ignored in this state
            // TODO code repetition
            if (!Physics.Linecast(eye.position, temp.target_interest.c.position + Vector3.up, vecteur.Solid))
                t -= Time.deltaTime;
            else
                stop();
        }
    }

    public class wake : act {
        [link]
        skin skin;

        public override priority priority => priority.action;

        protected override void _start() {
            skin.play ( new skin.animation ( anim.wake, this ) { end = stop } );
        }
    }

    
    [path("mecha")]
    public class wake_up : acting.first {
        [link]
        wake wake;

        protected override act get_act() {
            return wake;
        }
    }

    public class temp_target : moon {
        public warrior target_interest;
    }
}