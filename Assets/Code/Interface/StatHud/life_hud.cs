using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code {
    [need_ready]
    public class life_hud : graphic {

        life life;
        RectTransform container;

        // base
        HeartGraphic layer0;
        // glow
        HeartGraphic layer1;

        int hot_qstart; float hot_qend;

        public life_hud ( life life ) => this.life = life;

        public void start ( RectTransform _container ) {
            container = _container;

            ready_for_tick ();
            phoenix.core.start_action ( this );
        }

        protected override void _start() {
            layer0 = res.go.instantiate ( GO.heart_hp ).GetComponent <HeartGraphic> ();
            layer0.transform.SetParent ( container, false );
            layer0.rectTransform.anchoredPosition = Vector2.zero;

            layer1 = res.go.instantiate ( GO.heart_hp_glow ).GetComponent <HeartGraphic> ();
            layer1.transform.SetParent ( container, false );
            layer1.rectTransform.anchoredPosition = Vector2.zero;

            layer0._vh = _l0;
            layer1._vh = _l1;
        }

        protected override void _step() {
            check_hot ();
            // TODO, canvas group for performance
            layer0.SetVerticesDirty ();
            layer1.SetVerticesDirty ();
        }

        const float lerp_speed = 5;
        void check_hot () {
            int _hot_qstart = 0;
            life_point [] red_hearts = life.get_red;
             for (int i = 0; i < red_hearts.Length; i++) {
                if ( red_hearts [i].q0.is_hot ) {
                    _hot_qstart = i * 4;
                    break;
                }
                if ( red_hearts [i].q1.is_hot ) {
                    _hot_qstart = 1 + i * 4;
                    break;
                }
                if ( red_hearts [i].q2.is_hot ) {
                    _hot_qstart = 2 + i * 4;
                    break;
                }
                if ( red_hearts [i].q3.is_hot ) {
                    _hot_qstart = 3 + i * 4;
                    break;
                }
            }

            if ( _hot_qstart != -1 )
            hot_qstart = _hot_qstart;

            float hot_q_end_target = hot_qstart;

            for (int i = red_hearts.Length - 1 ; i >= 0; i--) {
                if ( red_hearts [i].q3.is_hot ) {
                    hot_q_end_target = 1 + 3 + i * 4;
                    break;
                }
                if ( red_hearts [i].q2.is_hot ) {
                    hot_q_end_target = 1 + 2 + i * 4;
                    break;
                }
                if ( red_hearts [i].q1.is_hot ) {
                    hot_q_end_target = 1 + 1 + i * 4;
                    break;
                }
                if ( red_hearts [i].q0.is_hot ) {
                    hot_q_end_target = 1 + i * 4;
                    break;
                }
            }

            if ( hot_q_end_target > hot_qend )
            hot_qend = hot_q_end_target;

            hot_qend = Mathf.Lerp ( hot_qend, hot_q_end_target, lerp_speed * Time.deltaTime );
        }

        const float heart_size = 16;
        void _l0 ( VertexHelper vh ) {
            
            // black heart
            life_point [] black_hearts = life.get_black;
            Color color = Color.black;

            for (int i = 0; i < black_hearts.Length; i++){
                if (black_hearts [i].count > 0)
                HeartGraphic.DrawHeartQuarter ( vh, 0, color, i * heart_size, heart_size );

                if (black_hearts [i].count > 1)
                HeartGraphic.DrawHeartQuarter ( vh, 1, color, i * heart_size, heart_size );

                if (black_hearts [i].count > 2)
                HeartGraphic.DrawHeartQuarter ( vh, 2, color, i * heart_size, heart_size );

                if (black_hearts [i].count > 3)
                HeartGraphic.DrawHeartQuarter ( vh, 3, color, i * heart_size, heart_size );
            }

            // red heart
            life_point [] red_hearts = life.get_red;
            color = Color.red;

            for (int i = 0; i < red_hearts.Length; i++){
                if (red_hearts [i].count > 0)
                HeartGraphic.DrawHeartQuarter ( vh, 0, color, i * heart_size, heart_size );

                if (red_hearts [i].count > 1)
                HeartGraphic.DrawHeartQuarter ( vh, 1, color, i * heart_size, heart_size );

                if (red_hearts [i].count > 2)
                HeartGraphic.DrawHeartQuarter ( vh, 2, color, i * heart_size, heart_size );

                if (red_hearts [i].count > 3)
                HeartGraphic.DrawHeartQuarter ( vh, 3, color, i * heart_size, heart_size );
            }

            // glow base white or red hearts
            color = Color.white;

            if (hot_qstart != Mathf.RoundToInt(hot_qend)) {
                for (int i = 0; i < red_hearts.Length; i++) {
                    if ( (i*4) >= hot_qstart && (i*4) < hot_qend )
                        HeartGraphic.DrawHeartQuarter(vh, 0, color, i * heart_size, heart_size);

                    if ( (1+i*4) >= hot_qstart && (2+i*4) < hot_qend )
                        HeartGraphic.DrawHeartQuarter(vh, 1, color, i * heart_size, heart_size);

                    if ( (2+i*4) >= hot_qstart && (2+i*4) < hot_qend )
                        HeartGraphic.DrawHeartQuarter(vh, 2, color, i * heart_size, heart_size);

                    if ( (3+i*4) >= hot_qstart && (3+i*4) < hot_qend )
                        HeartGraphic.DrawHeartQuarter(vh, 3, color, i * heart_size, heart_size);
                }
            }
        }

        // glow layer
        void _l1 ( VertexHelper vh ) {
            life_point [] red_hearts = life.get_red;
            Color color = Color.red;

            if (hot_qstart != Mathf.RoundToInt(hot_qend)) {
                for (int i = 0; i < red_hearts.Length; i++) {
                    if ( (i*4) >= hot_qstart && (i*4) < hot_qend )
                        HeartGraphic.DrawHeartQuarter(vh, 0, color, i * heart_size, heart_size);

                    if ( (1+i*4) >= hot_qstart && (2+i*4) < hot_qend )
                        HeartGraphic.DrawHeartQuarter(vh, 1, color, i * heart_size, heart_size);

                    if ( (2+i*4) >= hot_qstart && (2+i*4) < hot_qend )
                        HeartGraphic.DrawHeartQuarter(vh, 2, color, i * heart_size, heart_size);

                    if ( (3+i*4) >= hot_qstart && (3+i*4) < hot_qend )
                        HeartGraphic.DrawHeartQuarter(vh, 3, color, i * heart_size, heart_size);
                }
            }
        }

        protected override void _stop() {
            container.ClearChildren ();
        }
    }
}