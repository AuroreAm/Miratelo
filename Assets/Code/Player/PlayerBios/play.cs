using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    [need_ready]
    public class play : bios {
        actor main_actor;

        public play _(actor _main_actor) {
            main_actor = _main_actor;

            ready_for_tick();
            return this;
        }

        protected override void _start() {
            frame_actor();
        }

        void frame_actor() {
            camera.o.start_player_camera(main_actor.system.get<character>());
        }

        protected override void _step() {
            if ( Input.GetKeyDown (KeyCode.R) ) {
                var l = new life_hud ( main_actor.system.get<health> ().primary as life);
                l.start ( ui.o.player_hud.heart_container );

                ui.o.player_hud.bind_stamina ( main_actor.system.get <stamina> () );
            }
        }
    }

    [path("game")]
    public class start_play : action {
        [link]
        morai bios;

        [link]
        play play;

        [export]
        public term main_actor;

        protected override void _start() {
            bios.change(play._(xenos.get_actor(main_actor)));
            stop();
        }
    }
}