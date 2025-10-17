using Lyra;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Triheroes.Code {
    public static class game {
        public static void load_game ( GameData data ) {
            load_phoenix ();
        }

        static void load_phoenix () {
            var g = new GameObject ();
            g.AddComponent <phoenix> ();
        } 
    }
}