using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    [inked]
    public class sword : weapon {
        [link]
        character c;
        [link]
        matter_registry registry;

        public float sv_to_vu => vu_per_mu * velocity_factor_length * length;
        public const float vu_per_mu = 0.01f;
        public const float velocity_factor_length = 0.5f;

        public Vector3 position => c.position;
        public Vector3 tip_position => c.position + c.gameobject.transform.forward * length;

        public float length { get; private set; }

        public matter matter => registry.matter;

        slash.w w;

        public class ink : ink<sword> {
            public ink(float length, slash.w slash) {
                o.length = length;
                o.w = slash;
            }
        }

        public void slash(float sv, slay.path path, float duration) {
            w.fire(this, sv * sv_to_vu, path, duration);
        }

        public void enable_parry() {
            c.gameobject.layer = vecteur.ATTACK;
        }

        public void disable_parry() {
            c.gameobject.layer = 0;
        }
    }
}