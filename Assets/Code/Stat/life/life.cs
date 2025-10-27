using System;
using UnityEngine;

namespace Triheroes.Code {

    public class life : health_system.primary {
        public const int heart_q = 4;
        public const float heart_value = 2;
        public const float unit = 0.5f;
        public const float hot_duration = 0.5f;

        flesh matter;
        public int max_q { private set; get; }
        public int hp_q;

        public float hot { private set; get; }
        public int hot_q { private set; get; }

        public life ( int heart_count ) {
            max_q = heart_count * heart_q;
            hp_q = max_q;
        }

        protected override void _ready() {
            matter = a_new <flesh> ();
            hud = new life_hud (this);
        }

        protected override void _step() {
            if (hot > 0)
            hot -= Time.deltaTime;
            if (hot < 0)
            hot = 0;
        }

        public override void damage(damage damage) {
            damage = matter.reaction ( damage );

            int damage_q = Mathf.FloorToInt ( ( damage.value + eps ) / unit  );

            if ( damage_q > 0 && hot == 0 ) {
                hot_q = hp_q;
                hot = hot_duration;
            }

            hp_q = Mathf.Max ( hp_q - damage_q, 0 );
        }
    }

    public class life_hud : health_system_hud {
        life life;
        int hp;
        float last_hot_q;

        public life_hud ( life _life ) {
            life = _life;
            last_hot_q = life.hp_q;
        }

        const float lerp_speed = 5;
        public override void _draw(UILayer l0, UILayer l1) {
            int red = Math.Max (0,life.hp_q - life.max_q / 2);
            int black = life.hp_q - red;

            for (int i = 0; i < black; i++) {
                draw_quarter_heart ( l0, i % 4, Color.black, Mathf.Floor ( i / 4 ) * 16, 16 );
            }

            for (int i = 0; i < red; i++) {
                draw_quarter_heart ( l0, i % 4, Color.red, Mathf.Floor ( i / 4 ) * 16, 16 );
            }

            if ( life.hot > 0 ) 
                last_hot_q = life.hot_q;
            else
                last_hot_q = Mathf.Lerp ( last_hot_q, life.hp_q, lerp_speed * Time.deltaTime );

            int hot_q = Mathf.RoundToInt ( last_hot_q );

            int red_hot_q = Math.Max (0,hot_q - life.max_q / 2);
            for (int i = red; i < red_hot_q; i++) {
                draw_quarter_heart ( l0, i % 4, Color.white, Mathf.Floor ( i / 4 ) * 16, 16 );
                draw_quarter_heart ( l1, i % 4, Color.red, Mathf.Floor ( i / 4 ) * 16, 16 );
            }
        }

        void draw_quarter_heart ( UILayer l, int quarter_id, Color color, float pos_x, float size = 16 ) {
            float half = size / 2; float half_uv = .5f;
            float y = 0; float x = 0;
            float u = 0; float v = 0;

            switch ( quarter_id ) {
                case 0 : // top-left
                y = 0; x = 0; u = 0; v = half_uv;
                break;

                case 1 : // bottom-left
                y = -half; x = 0; u = 0; v = 0;
                break;

                case 2 : // bottom-right
                y = -half; x = half; u = half_uv; v = 0;
                break;

                case 3 : // top - right
                y = 0; x = half; u = half_uv; v = half_uv;
                break;
            }

            l.draw_square ( new Vector3 (x + pos_x,y,half), l.tile_of (tl.heart), color, new Vector4 ( u,v,half_uv,half_uv ) );
        }
    }

    /*
    public class life : health_system.primary {

        HP4vessel red;
        HP4vessel black;

        flesh matter;

        public life ( int point_count ) {
            red = new HP4vessel ( point_count );
            black = new HP4vessel ( point_count );
        }

        protected sealed override void _ready() {
            matter = a_new <flesh> ();
        }

        public override void damage ( damage damage ) {
            damage = matter.reaction ( damage );
            
            int quarter_count = Mathf.FloorToInt ( ( damage.value + eps ) / HP4.one.value );

            quarter_count = red.damage ( quarter_count );
            black.damage ( quarter_count );
        }

        protected override void _step() {
            red.tick ( Time.deltaTime );
            black.tick ( Time.deltaTime );
        }

        public HP4 [] get_red => red.points;
        public HP4 [] get_black => black.points;

    }*/
}