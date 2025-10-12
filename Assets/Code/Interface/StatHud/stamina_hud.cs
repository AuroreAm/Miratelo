using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    [need_ready]
    public class stamina_hud : graphic {
    
        stamina stamina;
        RectTransform container;

        // base
        StaminaGraphic layer0;
        // glow
        StaminaGraphic layer1;

        public stamina_hud (stamina stamina) => this.stamina = stamina;

        public void start ( RectTransform _container ) {
            container = _container;

            ready_for_tick ();
            phoenix.core.start_action ( this );
        }

        protected override void _start() {
            layer0 = instantiate_graphic_to_container <StaminaGraphic> ( GO.stamina_dot, container );
            layer1 = instantiate_graphic_to_container <StaminaGraphic> ( GO.stamina_dot_glow, container );

            layer0._vh = _l0;
            layer1._vh = _l1;
        }

        protected override void _step() {
            layer0.SetVerticesDirty ();
            layer1.SetVerticesDirty ();
        }
 
        const float radius = 8;
        const float dot_size = 8;
        const float dot_size2 = 7;
        
        void _l0 ( VertexHelper vh ) { 

            Color color;

            if (stamina.state == stamina.fatigue) {
                color = Color.red;
                for (int i = 0; i < stamina.max_active; i++)
                    draw_dot ( vh, i, stamina.red_ui, color, dot_size );

                color = Color.white;
                for (int i = 0; i < stamina.max_active; i++)
                    draw_dot ( vh, i, stamina.ghost_ui, color, dot_size2 );

                return;
            }

            if ( stamina.state == stamina.hot ) {
                color = Color.white;
                for (int i = Mathf.FloorToInt ( stamina.green_ui ); i < stamina.max_active; i++)
                draw_dot ( vh, i, Mathf.Lerp ( stamina.green_ui, stamina.hot_ui, stamina.cooldown_ui / stamina.cooldown_duration ), color, dot_size );
            }

            color = stamina.state == stamina.hot ? Color.cyan : new Color (0,.5f,1);

            for (int i = 0; i < stamina.max_active; i++)
                draw_dot ( vh, i, stamina.green_ui, color, dot_size );
        }

        void _l1 ( VertexHelper vh ) {
            Color color;
            if (stamina.state == stamina.fatigue) {
                color = new Color ( 1, .64f, 0 );
                for (int i = 0; i < stamina.max_active; i++)
                    draw_dot ( vh, i, stamina.ghost_ui, color, dot_size2 );

                return;
            }

            if ( stamina.state == stamina.hot ) {
                color = Color.red;
                for (int i = Mathf.CeilToInt ( stamina.green_ui ); i < stamina.max_active; i++)
                draw_dot ( vh, i, Mathf.Lerp ( stamina.green_ui, stamina.hot_ui, stamina.cooldown_ui / stamina.cooldown_duration ), color, dot_size );
            }
        }

        void draw_dot ( VertexHelper vh, int i, float value, Color color, float dot_size ) {
            float angle = ( 360f / stamina.max_active ) * i;
            float size = Mathf.Clamp01 ( value - i ) * dot_size;
            StaminaGraphic.DrawStamina ( vh, angle, radius, color, size );
        }

    }
}