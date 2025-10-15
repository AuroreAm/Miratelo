using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public class shielding : health_system.sub {
        const float hot_duration = 2;
        const float penetration_factor = 0.5f;
        const float penetration_max_HP_factor = 0.1f;
        const float regen_speed = 10;

        float max;
        float penetrationHP_max;
        float HP;
        float penetrationHP;
        float hot;
        
        metal matter;

        public shielding (int _max, metal _matter) {
            matter = _matter;
            max = _max;
            HP = _max;
            penetrationHP_max = _max * penetration_max_HP_factor;
            penetrationHP = penetrationHP_max;
        }

        public override void damage ( damage damage ) {
            damage = matter.reaction (damage);

            float raw = Mathf.FloorToInt(damage.value + eps);
            hot = hot_duration;

            HP -= raw;

            if (penetrationHP > 0)
                penetrationHP -= raw;
            else {
                damage.value *= penetration_factor;
                previous.damage ( damage );
            }

            Debug.Log ( $"{HP} - {penetrationHP}" );
        }

        protected override void _step() {
            if (hot > 0) {
                hot -= Time.deltaTime;
                if (hot < 0)
                    hot = 0;
            }
            else if (penetrationHP < penetrationHP_max) {
                penetrationHP += regen_speed * Time.deltaTime;
                if (penetrationHP > penetrationHP_max)
                    penetrationHP = penetrationHP_max;
            }
        }
    }
}