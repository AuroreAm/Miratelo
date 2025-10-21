using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public class play : bios {
        int ptr;

        [link]
        main_actors mains;

        protected override void _start() {
            frame_actor ( mains [ptr] );
        }

        void frame_actor ( warrior a ) {

            camera.o.start_player_camera ( a.c );
            ui.o.health_hud.start ( a.system.get <health> (), a.system.get <stamina> () );

            a.system.get <behavior> ().set_behavior ( sh.player );

        }
    }

    [path("scene")]
    public class start_play : action {

        [link]
        morai morai;

        [link]
        play play;
        
        protected override void _start() {
            morai.change ( play );
            stop ();
        }
    }
}