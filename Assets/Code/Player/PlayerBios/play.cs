using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public class play : bios {
        int ptr;

        [link]
        main_actors mains;
        [link]
        codex codex;

        actor player;
        photon p_photon;
        interest current_interest;

        interval interest_interval;

        public play () {
            interest_interval = new interval ( low_step_interest, .4f );
        }

        protected override void _start() {
            frame_actor ( mains [ptr] );
        }

        protected override void _step() {
            interest_interval.tick ( Time.deltaTime );
        }

        void frame_actor ( warrior a ) {
            player = (actor) a;
            p_photon = a.system.get <photon> ();

            camera.o.start_player_camera ( a.c );
            ui.o.player_hud.frame_player ( (actor) a );

            a.system.get <behavior> ().set_behavior ( sh.player );
        }

        void low_step_interest () {
            interest near = codex.near_to ( player.position, 3 );
            if ( near != current_interest ) {
                if ( current_interest != null )
                p_photon.radiate ( new _exit_interest ( current_interest ) );
                if ( near != null )
                p_photon.radiate ( new _enter_interest ( near ) );
            }

            current_interest = near;
        }
    }

    public struct _enter_interest {
        public interest interest;

        public _enter_interest ( interest _interest ) {
            interest = _interest;
        }
    }

    public struct _exit_interest {
        public interest interest;

        public _exit_interest ( interest _interest ) {
            interest = _interest;
        }
    }

    [path("scene")]
    public class start_play : action {

        [link]
        game_bios game_bios;

        [link]
        play play;
        
        protected override void _start() {
            game_bios.change ( play );
            stop ();
        }
    }
}