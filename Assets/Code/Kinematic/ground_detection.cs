using Lyra;
using UnityEngine;

namespace Triheroes.Code.Axeal {
    [star(order.axeal_ground)]
    public class ground : star.main {

        bool on_ground = true;
        public bool raw;

        public void set ( bool is_grounded )
        {
            on_ground = is_grounded;
        }

        public static implicit operator bool(ground a)
        {
            return a.on_ground;
        }

        public Vector3 normal = Vector3.up;

        [link]
        capsule capsule;

        protected override void _step() {
            raw = false;
            set(Physics.SphereCast(
                capsule.coord.position + new Vector3(0, capsule.r + 0.1f, 0),
                capsule.r, Vector3.down,
                out RaycastHit hit,
                0.5f,
                vecteur.SolidCharacter));

            if (on_ground) {
                normal = hit.normal;
                raw = hit.distance <= 0.2f;
            }
        }
    }
}