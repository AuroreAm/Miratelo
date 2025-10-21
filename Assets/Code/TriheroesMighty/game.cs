using Lyra;
using UnityEngine;
using System;

namespace Triheroes.Code {
    public static class game {
        public static void load_game ( ActionPaper game ) {
            load_phoenix ();

            action game_init = game.Write ( phoenix.core );
            phoenix.core.start_action ( game_init );
        }

        static void load_phoenix () {
            var g = new GameObject ("Phoenix");
            g.AddComponent <phoenix> ();
            scene_start ();
        }

        static event Action scene_start = delegate { };
        static public event Action _scene_start
        {
            add { scene_start += value; }
            remove { scene_start -= value; }
        }
    }
}