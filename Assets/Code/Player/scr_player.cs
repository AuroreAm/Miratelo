using System.Collections.Generic;
using Lyra;
using Triheroes.Code;

namespace Triheroes.Code {
    public static partial class scr {
        public static void add_player(actor player, script_builder sb) {

            // player movement, this is now just used for capsule based character.
            sb.a <player_move> ();
            sb.a <player_jump> ();

            // skills controller
            skills s = player.system.get <skills> ();
            if ( s.contains <decoherence_blink> () )
            sb.a <player_decoherence_blink> ();

            // face idle player
            sb.a <face_idle> ();

            // normal reaction of an actor
            add_actor ( sb );
        }
    }
}