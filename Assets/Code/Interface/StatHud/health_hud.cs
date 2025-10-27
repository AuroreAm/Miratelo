using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public abstract class health_system_hud {
        public abstract void _draw ( UILayer l0, UILayer l1 );
    }

    [need_ready]
    public class health_hud : graphic_element {
        art hud_art;

        public health_hud ( RectTransform container ) {
            UILayer l0 = container.GetChild (0).GetComponent <UILayer> ();
            UILayer l1 = container.GetChild (1).GetComponent <UILayer> ();

            hud_art = new art (); hud_art.layers.Add (l0); hud_art.layers.Add (l1);
        }

        public void start ( health health ) {
            hud_art.health = health;
            
            if (!on) {
                ready_for_tick ();
                phoenix.core.start_action ( this );
            }
        }

        public void end () {
            if ( on )
            stop ();
        }

        protected override void _step() {
            hud_art.draw ();
        }

        class art : UILayer.base_art {
            public health health;
            protected override void _draw() {
                health.primary.hud._draw (layers [0], layers [1]);
            }
        }
    }

    [need_ready]
    public class energy_hud : graphic_element {
        public RectTransform container {private set; get;}
        art hud_art;

        public stamina stamina => hud_art.stamina;

        public energy_hud ( RectTransform _container ) {
            UILayer l0 = _container.GetChild (0).GetComponent <UILayer> ();
            UILayer l1 = _container.GetChild (1).GetComponent <UILayer> ();
            
            container = _container;
            hud_art = new art ();
            hud_art.layers.Add (l0);
            hud_art.layers.Add (l1);
        }

        public void start ( stamina stamina ) {
            hud_art.stamina = stamina;

            if (!on) {
            ready_for_tick ();
            phoenix.core.start_action (this);
            }
        }

        public void end () {
            if (on)
            stop ();
        }

        protected override void _step() {
            hud_art.draw ();
        }

        class art : UILayer.base_art {
            public stamina stamina;

            protected override void _draw() {
                stamina.hud._draw (layers[0], layers[1]);
            }
        }
    }

}