using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public struct damage {
        public Vector3 point;
        public float value {get; set;}
        public matter damager;

        public damage ( Vector3 point, matter matter, float vu, type type ) {
            damager = matter;
            this.point = point;
            value = matter.mu * matter.damage_factor * vu * type.factor;
        }

        public struct type {
            public float factor ;
        }

        public static readonly type shock_wave = new type { factor = 0.005f };
        public static readonly type diffuse = new type { factor = 0.01f };
        public static readonly type pierce = new type { factor = 1 };
        public static readonly type slash = new type { factor = 2 };
    }

}