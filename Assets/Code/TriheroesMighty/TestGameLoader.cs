using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class TestGameLoader : moon {
        public GameData Game;

        void Awake () {
            game.load_game ( Game );
        }
    }
}