using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [need_ready]
    public class health_hud : graphic_element {

        heart_hud heart_hud;
        health_point target;

        public health_hud ( RectTransform heart_container, RectTransform heart_prefab, float heart_size ) {
            heart_hud = new heart_hud ( heart_container, heart_prefab, heart_size );
        }

        public void start ( health_point _target ) {
            target = _target;

            ready_for_tick ();
            phoenix.core.start_action ( this );
        }

        protected override void _step () {
            heart_hud.set ( target.HP );
        }

    }
}