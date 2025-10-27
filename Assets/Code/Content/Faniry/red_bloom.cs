using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;

namespace Triheroes.Code {
    public class red_bloom : skill, act_handler {
        public bool ready;

        [link]
        stamina stamina;

        [link]
        motor motor;

        [link]
        dash_red_bloom bloom;

        [link]
        decoherence_air air;

        public bool spam () {
            bool success = false;

            if ( stamina.has_green () && ready ) {
                if (air.on) return success;

                success = motor.start_act ( bloom, this );

                if ( success )
                stamina.use (1);
            }

            return success;
        }

        public void stop () {
            if (active)
            motor.stop_act (this);
        }

        public void _act_end(act a, act_status status) {
        }

        public bool active => motor.act == bloom;

        public bool on => true;
    }

    public class player_red_bloom : action {
        [link]
        skills s;

        [link]
        ground ground;

        red_bloom skill => s.get <red_bloom> ();

        protected override void _step() {
            if ( ground ) {
                skill.ready = true;
            }

            if ( !ground && player.dash.down ) {
                if (skill.spam ())
                skill.ready = false;
            }

            if ( skill.active && player.dash.up )
                skill.stop ();
        }
    }

    public class dash_red_bloom : act {
        [link]
        decoherence_trail trail;
        public override priority priority => priority.action3;
        dash_core core;

        [link]
        character c;

        int illusion;
        illusion.w red_bloom;

        protected override void _ready() {
            red_bloom = res.illusions.q ( sp.red_bloom ).get_w ();

            core = with ( new dash_core ( sh.red_blink ) );
            core.set_animations ( anim.dash_forward );
            core.duration = .6f;
            core.lenght = 6;
        }

        protected override void _start() {
            core.prepare ( direction.forward );
            core.start ( this, true, true );
            link ( trail );
            illusion = red_bloom.fire (c.position);
        }

        protected override void _step() {
            core.skin_stand_update ();
            red_bloom.set_position ( illusion, c.position );

            if ( core.done ) stop ();
        }

        protected override void _stop() {
            core.stop ();
        }
    }
}
