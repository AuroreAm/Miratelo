using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code {
    [need_ready]
    public class life_hud : graphic {

        life life;
        RectTransform container;

        // base
        HeartHP layer0;
        // glow
        HeartHP layer1;

        public life_hud ( life life ) => this.life = life;

        public void start ( RectTransform _container ) {
            container = _container;

            ready_for_tick ();
            phoenix.core.start_action ( this );
        }

        protected override void _start() {
            layer0 = res.go.instantiate ( GO.heart_hp ).GetComponent <HeartHP> ();
            layer0.transform.SetParent ( container, false );
            layer0.rectTransform.anchoredPosition = Vector2.zero;

            layer1 = res.go.instantiate ( GO.heart_hp_glow ).GetComponent <HeartHP> ();
            layer1.transform.SetParent ( container, false );
            layer1.rectTransform.anchoredPosition = Vector2.zero;

            layer0._vh = _l0;
            layer1._vh = _l1;
        }

        protected override void _step() {
            // TODO, canvas group for performance
            layer0.SetVerticesDirty ();
            layer1.SetVerticesDirty ();
        }

        void _l0 ( VertexHelper vh ) {
            
            // black heart
            life_point [] black_hearts = life.get_black;
            Color color = Color.black;

            for (int i = 0; i < black_hearts.Length; i++){
                if (black_hearts [i].count > 0)
                HeartHP.DrawHeartQuarter ( vh, 0, color, i * 16, 16 );

                if (black_hearts [i].count > 1)
                HeartHP.DrawHeartQuarter ( vh, 1, color, i * 16, 16 );

                if (black_hearts [i].count > 2)
                HeartHP.DrawHeartQuarter ( vh, 2, color, i * 16, 16 );

                if (black_hearts [i].count > 3)
                HeartHP.DrawHeartQuarter ( vh, 3, color, i * 16, 16 );
            }

            // red heart
            life_point [] red_hearts = life.get_red;
            color = Color.red;

            for (int i = 0; i < red_hearts.Length; i++){
                if (red_hearts [i].count > 0)
                HeartHP.DrawHeartQuarter ( vh, 0, color, i * 16, 16 );

                if (red_hearts [i].count > 1)
                HeartHP.DrawHeartQuarter ( vh, 1, color, i * 16, 16 );

                if (red_hearts [i].count > 2)
                HeartHP.DrawHeartQuarter ( vh, 2, color, i * 16, 16 );

                if (red_hearts [i].count > 3)
                HeartHP.DrawHeartQuarter ( vh, 3, color, i * 16, 16 );
            }

            // glow base white or red hearts
            color = Color.white;
            for (int i = 0; i < red_hearts.Length; i++) {
                if ( red_hearts [i].q0.hot > 0 )
                HeartHP.DrawHeartQuarter ( vh, 0, color, i * 16, 16 );

                if ( red_hearts [i].q1.hot > 0 )
                HeartHP.DrawHeartQuarter ( vh, 1, color, i * 16, 16 );

                if ( red_hearts [i].q2.hot > 0 )
                HeartHP.DrawHeartQuarter ( vh, 2, color, i * 16, 16 );

                if ( red_hearts [i].q3.hot > 0 )
                HeartHP.DrawHeartQuarter ( vh, 3, color, i * 16, 16 );
            }

        }

        // glow layer
        void _l1 ( VertexHelper vh ) {
            life_point [] red_hearts = life.get_red;
            Color color = Color.red;

            for (int i = 0; i < red_hearts.Length; i++) {
                if ( red_hearts [i].q0.hot > 0 )
                HeartHP.DrawHeartQuarter ( vh, 0, color, i * 16, 16 );

                if ( red_hearts [i].q1.hot > 0 )
                HeartHP.DrawHeartQuarter ( vh, 1, color, i * 16, 16 );

                if ( red_hearts [i].q2.hot > 0 )
                HeartHP.DrawHeartQuarter ( vh, 2, color, i * 16, 16 );

                if ( red_hearts [i].q3.hot > 0 )
                HeartHP.DrawHeartQuarter ( vh, 3, color, i * 16, 16 );
            }
        }

        protected override void _stop() {
            container.ClearChildren ();
        }
    }
}