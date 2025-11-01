using Lyra;

namespace Triheroes.Code {
    public static partial class scr {
        public static void add_player(actor player, sb sb) {

            // player movement, this is now just used for capsule based character.
            sb.a <player_move> ();
            sb.a <player_jump> ();

            // equip
            sb.a <player_equip> ();

            // skills controller
            skills s = player.system.get <skills> ();

            if ( s.contains <decoherence_blink> () )
            sb.a <player_decoherence_blink> ();

            if ( s.contains <red_bloom> () )
            sb.a <player_red_bloom> ();

            if ( s.contains <SS1> () )
            sb.a <player_sword> ();

            // face idle player
            sb.a <face_idle> ();
        }
    }
}