using Lyra;
using UnityEngine;

namespace Triheroes.Code
{/*
    [need_ready]
    [inked]
    public class player_hud : graphic_element {

        public RectTransform heart_container;
        public RectTransform alt_heart_container;
        RectTransform stamina_container;

        stamina monitered;

        public class ink : ink <player_hud> {
            public ink ( RectTransform _heart_container, RectTransform _stamina_container, RectTransform _alt_heart_container ) { 
                o.heart_container = _heart_container;
                o.stamina_container = _stamina_container;
                o.alt_heart_container = _alt_heart_container;
            }
        }

        public void bind_stamina (stamina stamina) {
            monitered = stamina;

            ready_for_tick ();
            phoenix.core.start_action (this);
        }

        protected override void _start() {
            new stamina_hud ( monitered ).start ( stamina_container );
        }

        protected override void _step() {
            stamina_container.anchoredPosition = monitered.hud.stamina (); 
        }
    }*/
}