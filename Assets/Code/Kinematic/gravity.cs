using Lyra;
using UnityEngine;

namespace Triheroes.Code.Axeal {
    [star (order.axeal_gravity)]
    public class gravity : star.main {
        [link]
        ground ground;
        [link]
        axeal a;
        [link]
        capsule capsule;

        public static implicit operator float(gravity gravity) => gravity.g;

        float mass => a.m;
        float g;

        public const float fake_acc = 40;

        protected override void _start() {
            g = -0.2f;
        }

        protected override void _step() {
            // add gravity force //  falling velocity limited when it reach terminal velocity
            if (g > -1000)
                g += Physics.gravity.y * Time.deltaTime * fake_acc;

            if (ground.raw && g < 0 && Vector3.Angle(Vector3.up, ground.normal) <= 45)
                g = -0.2f;

            Vector3 force = new Vector3(0, g * Time.deltaTime, 0);

            // TODO: fix character can't fall when there's another character on the ground
            if (Vector3.Angle(Vector3.up, ground.normal) > 45) {
                force = new Vector3 (ground.normal.x, -ground.normal.y, ground.normal.z) * force.magnitude;
                ground.normal = Vector3.up;
            }

            capsule.move ( force );
        }
    }
}