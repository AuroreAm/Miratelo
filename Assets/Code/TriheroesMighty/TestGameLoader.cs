using System;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class TestGameLoader : MonoBehaviour {

        public ActionPaper TestGame;

        void Awake () {
            game.load_game ( TestGame );
        }
    }
}