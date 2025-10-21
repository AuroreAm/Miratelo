using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    [need_ready]
    public class health_hud : action {

        art hud_art;

        public class ink : ink <health_hud> {
            public ink ( UILayer l0, UILayer l1 ) {
                o.hud_art = new art ();
                o.hud_art.layers.Add (l0);
                o.hud_art.layers.Add (l1);
            }
        }

        public void start ( health health, stamina stamina ) {
            hud_art.health = health;
            hud_art.stamina = stamina;
            
            ready_for_tick ();
            phoenix.core.start_action ( this );
        }

        protected override void _step() {
            hud_art.draw ();
        }

        class art : UILayer.base_art {
            public health health;
            public stamina stamina;
            protected override void _draw() {
                health.primary.hud._draw (layers [0], layers [1]);
                shift ( Vector2.down * 32 );
                stamina.hud._draw (layers [0], layers [1]);
            }
        }
    }

    public abstract class health_system_hud {
        public abstract void _draw ( UILayer l0, UILayer l1 );
    }
}