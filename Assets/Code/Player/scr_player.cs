using System.Collections.Generic;
using Lyra;
using Triheroes.Code;

namespace Triheroes.Code {
    public static partial class scr {
        public static void add_player(script_builder sb) {
            sb.a<player_move>();
            sb.a<player_jump>();
            sb.a<player_equip>();
            sb.a<player_sword>();
            sb.a<player_dash>();
            sb.a<player_bow>();
            sb.a<player_parry>();
            sb.a<player_sword_target>();
            sb.a<player_target> ();

            add_actor(sb);
        }
    }
}