using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    [inked]
    public class illusion : spectre {
        new ParticleSystem system;

        public class ink : ink<illusion> {
            public ink(ParticleSystem system) {
                o.system = system;
            }
        }

        protected override void __ready() {
            system.gameObject.SetActive(false);
        }

        static Vector3 _position;

        public class w : bridge {
            public int fire(Vector3 position) {
                _position = position;
                return orion.rent(name);
            }

            public void set_position (int id, Vector3 position) {
                orion.get <illusion> (name, id).system.transform.position = position;
            }
        }

        protected override void _start() {
            system.gameObject.transform.position = _position;
            system.time = 0;
            system.gameObject.SetActive(true);
        }

        protected override void _step() {
            if ( !system.IsAlive () )
           virtus.return_ ();
        }

        protected override void _stop() {
            system.gameObject.SetActive(false);
        }
    }
}
