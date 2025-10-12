using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    [inked]
    public class ui : moon {
        RectTransform main;
        public static float wd => o.main.rect.width;
        public static float hd => o.main.rect.height;

        public static ui o;

        public class ink : ink <ui> {
            public ink (RectTransform main) {
                o.main = main;
            }
        }

        [link]
        public full_overlay full_overlay;

        [link]
        public player_hud player_hud;

        protected override void _ready() {
            o = this;
        }
    }
}