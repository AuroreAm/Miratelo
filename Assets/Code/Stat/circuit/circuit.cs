using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public class circuit : health_system.primary {
        public const int heart_q = 4;
        public const float heart_value = 2;
        public const float unit = 0.5f;
        public const float hot_duration = 0.5f;

        circuitery matter;
        public int max_q { private set; get; }
        public int hp_q;

        public float hot { private set; get; }
        public int hot_q { private set; get; }

        public circuit(int heart_count) {
            max_q = heart_count * heart_q;
            hp_q = max_q;
        }

        protected override void _ready() {
            matter = a_new < circuitery > ();
        }

        protected override void _step() {
            if (hot > 0)
                hot -= Time.deltaTime;
            if (hot < 0)
                hot = 0;
        }

        public override void damage(damage damage) {
            damage = matter.reaction(damage);

            int damage_q = Mathf.FloorToInt((damage.value + eps) / unit);

            if (damage_q > 0 && hot == 0) {
                hot_q = hp_q;
                hot = hot_duration;
            }

            hp_q = Mathf.Max(hp_q - damage_q, 0);
        }
            
        public bool damaged => hp_q == 0;
    }


    public class circuitery : matter {
        public override float damage_factor => .03f;
    }
}