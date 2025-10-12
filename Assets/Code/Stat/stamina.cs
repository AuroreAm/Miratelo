using Lyra;
using UnityEngine;

namespace Triheroes.Code {

    [inked]
    public class stamina : auto_stat {
        [link]
        public hud_pos hud;

        public int state {private set; get;}
        public const int idle = 0; public const int hot = 1; public const int fatigue = 2;

        public int max_active {get; private set;}
        float regen_speed;
        public float cooldown_duration {private set; get;}

        float red_stamina;
        float green_stamina;
        float ghost_stamina;
        float cooldown;

        public float hot_ui {private set; get;}
        public float green_ui => green_stamina;
        public float red_ui => red_stamina;
        public float ghost_ui => ghost_stamina;
        public float cooldown_ui => cooldown;

        public class ink : ink<stamina> {
            public ink(int _max, float _regen_speed = 1, float _cooldown_duration = 2) {
                o.max_active = _max;
                o.regen_speed = _regen_speed;
                o.cooldown_duration = _cooldown_duration;

                o.red_stamina = _max;
                o.green_stamina = _max;
            }
        }

        protected override void _ready() {
            phoenix.core.execute(this);
        }

        protected override void _step() {
            float dt = Time.deltaTime;
            switch (state) {
                case idle: handle_idle(dt); break;
                case hot: handle_hot(dt); break;
                case fatigue: handle_fatigue(dt); break;
            }
        }

        void to_idle() {
            ghost_stamina = 0;
            state = idle;
        }

        void to_fatigue() {
            ghost_stamina = 0;
            state = fatigue;
        }

        void to_hot () {
            ghost_stamina = 0;
            cooldown = cooldown_duration;
            state = hot;
        }

        void handle_idle(float dt) {
            // regen green
            if (green_stamina < max_active) {
                green_stamina += dt * regen_speed;
                if (green_stamina > max_active) green_stamina = max_active;
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
                red_stamina += dt * regen_speed;
                if (red_stamina > max_active) red_stamina = max_active;
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

        public bool has_green(int amount) => green_stamina >= amount;
        public bool has_any(int amount) => green_stamina >= amount || red_stamina >= amount;

        public bool use_green(int amount) {
            if (!has_green(amount)) return false;

            if ( state != hot )
            hot_ui = green_stamina;

            green_stamina -= amount;

            if (green_stamina > 0)
                to_hot();
            else to_fatigue();

            return true;
        }

        public bool use_forced(int amount) {
            if (!has_any(amount)) return false;

            float remaining = amount;

            // use green first
            if (green_stamina > 0) {
                 if ( state != hot )
                hot_ui = green_stamina;

                float green_used = Mathf.Min ( green_stamina, remaining );
                green_stamina -= green_used;
                remaining -= green_used;
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
}