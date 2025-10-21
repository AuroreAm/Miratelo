using Lyra;
using UnityEngine;

namespace Triheroes.Code {

    [inked]
    public class stamina : auto_stat {
        [link]
        public hud_pos hud_pos;

        public enum state { idle, hot, fatigue }
        public state status { private set; get; }

        public int max_active {get; private set;}
        float regen_speed;
        public float cooldown_duration {private set; get;}

        int red_stamina;
        int green_stamina;

        float cooldown;
        float regen;
        float ghost_stamina;

        public float hot_ui {private set; get;}
        public float green_ui => green_stamina;
        public float red_ui => red_stamina;
        public float ghost_ui => ghost_stamina;
        public float cooldown_ui => cooldown;
        public float regen_ui => regen;
        public stamina_hud hud {private set; get;}

        public class ink : ink <stamina> {
             public ink(int _max, float _regen_speed = 2, float _cooldown_duration = 2) {
                o.max_active = _max;
                o.regen_speed = _regen_speed;
                o.cooldown_duration = _cooldown_duration;

                o.red_stamina = _max;
                o.green_stamina = _max;
            }
        }

        protected override void _ready() {
            phoenix.core.execute(this);
            hud = new stamina_hud (this);
        }

        protected override void _step() {
            float dt = Time.deltaTime;
            switch (status) {
                case state.idle: handle_idle(dt); break;
                case state.hot: handle_hot(dt); break;
                case state.fatigue: handle_fatigue(dt); break;
            }
        }

        void to_idle() {
            regen = 0;
            ghost_stamina = 0;
            status = state.idle;
        }

        void to_fatigue() {
            regen = 0;
            ghost_stamina = 0;
            status = state.fatigue;
        }

        void to_hot () {
            ghost_stamina = 0;
            cooldown = cooldown_duration;
            status = state.hot;
        }

        void handle_idle(float dt) {
            // regen green
            if (green_stamina < max_active) {
                regen += dt * regen_speed;
                if ( regen >= 1 ) {
                    green_stamina ++;
                    regen -= 1;
                }
            }
        }

        void handle_hot(float dt) {
            cooldown -= dt;
            if (cooldown <= 0) {
                if (green_stamina > 0)
                    to_idle();
                else to_fatigue();
            }
        }

        void handle_fatigue(float dt) {
            // first refill red
            if (red_stamina < max_active) {
                regen += dt * regen_speed;
                if ( regen >= 1 ) {
                    red_stamina ++;
                    regen -= 1;
                }
                return;
            }

            // then refill ghost stamina
            if (ghost_stamina < max_active) {
                ghost_stamina += dt * regen_speed;
                if (ghost_stamina > max_active) ghost_stamina = max_active;
                return;
            }

            // when ghosts full, instant refill green and idle
            if (ghost_stamina >= max_active) {
                green_stamina = max_active;
                to_idle ();
            }
        }

        public bool has_green () => green_stamina > 0;

        public bool use (int amount) {
            if (!has_green ()) return false;

            int remaining = amount;

            // use green first
            if (green_stamina > 0) {
                if (status != state.hot)
                    hot_ui = green_stamina;

                float green_used = Mathf.Min(green_stamina, remaining);
                green_stamina -= (int) green_used;
                remaining -= (int) green_used;
            }

            // use red if still needed
            if (remaining > 0)
                red_stamina -= remaining;

            if (green_stamina > 0)
                to_hot();
            else to_fatigue();

            return true;
        }
    }

    public class stamina_hud : health_system_hud {
        stamina stamina;
        const float radius = 8;
        const float dot_size = 8;
        const float dot_size2 = 7;

        public stamina_hud ( stamina _stamina ) {
            stamina = _stamina;
        }

        public override void _draw(UILayer l0, UILayer l1) {
            Color color;

             if (stamina.status == stamina.state.fatigue) {
                color = Color.red;
                for (int i = 0; i < stamina.max_active; i++)
                    draw_dot ( l0, i, stamina.red_ui, color, dot_size );

                color = Color.white;
                for (int i = 0; i < stamina.max_active; i++)
                    draw_dot ( l0, i, stamina.ghost_ui, color, dot_size2 );
                color = new Color ( 1, .64f, 0 );
                for (int i = 0; i < stamina.max_active; i++)
                    draw_dot ( l1, i, stamina.ghost_ui, color, dot_size2 );

                return;
            }

            if ( stamina.status == stamina.state.hot ) {
                color = Color.white;
                for (int i = Mathf.FloorToInt ( stamina.green_ui ); i < stamina.max_active; i++)
                draw_dot ( l0, i, Mathf.Lerp ( stamina.green_ui, stamina.hot_ui, stamina.cooldown_ui / stamina.cooldown_duration ), color, dot_size );

                color = Color.red;
                for (int i = Mathf.FloorToInt ( stamina.green_ui ); i < stamina.max_active; i++)
                draw_dot ( l1, i, Mathf.Lerp ( stamina.green_ui, stamina.hot_ui, stamina.cooldown_ui / stamina.cooldown_duration ), color, dot_size );
            }

            color = stamina.status == stamina.state.hot ? Color.cyan : new Color (0,.5f,1);

            for (int i = 0; i < stamina.max_active; i++)
                draw_dot ( l0, i, stamina.green_ui + stamina.regen_ui, color, dot_size );
        }

        void draw_dot ( UILayer l, int i, float value, Color color, float dot_size ) {
            float angle = ( 360f / stamina.max_active ) * i;
            float size = Mathf.Clamp01 ( value - i ) * dot_size;
            draw_stamina ( l, size, angle, 16, color );
        }

        void draw_stamina ( UILayer l, float size, float angle, float distance, Color color ) {
            Vector3 xys = new Vector3 (
                Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
                Mathf.Sin(angle * Mathf.Deg2Rad) * distance,
                size
            );

            l.draw_square ( xys, l.tile_of (tl.stamina_dot), color );
        }
    }
}