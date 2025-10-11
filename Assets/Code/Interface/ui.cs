using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class ui : moon {
        public static ui o;

        [link]
        public full_overlay full_overlay;

        [link]
        public player_hud player_hud;

        protected override void _ready() {
            o = this;
        }
    }
}