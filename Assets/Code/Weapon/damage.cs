using UnityEngine;

namespace Triheroes.Code
{
    public struct damage {
        public Vector3 point;
        public float value {get; set;}
        public matter damager;
        Vector3 normal_vu;
        public damage_type type;

        public Vector3 normal => normal_vu.normalized;
        public float vu => normal_vu.magnitude;

        public damage ( Vector3 point, Vector3 normal, matter matter, float vu, damage_type type ) {
            damager = matter;
            this.point = point;
            normal_vu = normal.normalized * vu;
            this.type = type;
            value = matter.mu * matter.damage_factor * vu * type.factor;
        }

        public static readonly damage_type shock_wave = new damage_type { factor = 0.005f };
        public static readonly damage_type diffuse = new damage_type { factor = 0.01f };
        public static readonly damage_type pierce = new damage_type { factor = 1 };
        public static readonly damage_type slash = new damage_type { factor = 2 };
    }

    public struct damage_type {
        public float factor ;
    }

}