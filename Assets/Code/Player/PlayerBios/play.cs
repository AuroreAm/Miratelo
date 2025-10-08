using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [need_ready]
    public class play : bios
    {
        actor main_actor;

        public play _ ( actor _main_actor )
        {
            main_actor = _main_actor;

            ready_for_tick ();
            return this;
        }

        protected override void _start()
        {
            frame_actor ();
        }

        void frame_actor ()
        {
            camera.o.start_player_camera ( main_actor.system.get <character> () );
            ui.o.health_hud.start ( main_actor.system.get <health_point> () );
            ui.o.hud_Label.set ( main_actor.system.get <actor> () );
        }
    }

    [path("game")]
    public class start_play : action
    {
        [link]
        morai bios;

        [link]
        play play;

        [export]
        public term main_actor;

        protected override void _start ()
        {
            bios.change ( play._ ( pallas.get_actor ( main_actor ) ) );
            stop ();
        }
    }
}