using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    [inked]
    public class ui : moon {

        public static ui o;

        [link]
        public full_overlay full_overlay;

        public health_hud health_hud { private set; get; }
        public character_hud_label hud_Label { private set; get; }

        public class ink : ink < ui > {
            public ink ( Text label, RectTransform heart_container, RectTransform heart_prefab, float heart_size ) {
                o.hud_Label = new character_hud_label ( label );
                o.health_hud = new health_hud ( heart_container, heart_prefab, heart_size );
            }
        }

        protected override void _ready() {
            o = this;
        }

    }
}