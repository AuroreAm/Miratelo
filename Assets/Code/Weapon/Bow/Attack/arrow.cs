using System;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    [star(order.arrow)]
    public class arrow : virtus.star {
        public float speed { private set; get; }
        public Vector3 position { private set; get; }
        public Quaternion rotation { private set; get; }
        float timeleft;

        protected override void _start() {
            active_arrows.Add(this);

            timeleft = 5;
            position = _pos;
            rotation = _rot;
            speed = _spd;
        }

        protected override void _stop() {
            active_arrows.Remove(this);
        }


        #region fire
        static Vector3 _pos;
        static Quaternion _rot;
        static float _spd;

        public class w : bridge {
            public void fire(Vector3 pos, Quaternion rot, float spd) {
                _pos = pos;
                _rot = rot;
                _spd = spd;
                orion.rent(name);
            }
        }
        #endregion

        static List<arrow> active_arrows = new List<arrow>();
        public static void deflect(Vector3 position, Vector3 normal) {
            arrow to = null;
            foreach (var a in active_arrows)
                if (a.position == position)
                    to = a;

            if (to != null)
                to.rotation = vecteur.rot_direction_quaternion(Vector3.zero, normal);
        }

        protected override void _step() {
            cast();
            signal();
            lifetime();
        }

        void cast() {
            float spd = speed * Time.deltaTime;

            if (Physics.Raycast(position, vecteur.forward(rotation), out RaycastHit hit, spd, vecteur.SolidCharacterAttack)) {
                position += vecteur.forward(rotation) * (hit.distance - 0.1f);

                if (hit.collider.gameObject.layer == vecteur.DECOR || hit.collider.gameObject.layer == vecteur.STATIC) {
                    virtus.return_();
                    return;
                }

                if (pallas.contains(hit.collider.id())) {
                    pallas.radiate(hit.collider.id(), new perce(2, position));
                    virtus.return_();
                }
            }
            else
                position += vecteur.forward(rotation) * spd;
        }

        void signal() {
            if (Physics.Raycast(position, vecteur.forward(rotation), out RaycastHit hit, 2 * speed, vecteur.Character)) {
                if (pallas.contains(hit.collider.id()))
                    pallas.radiate(hit.collider.id(), new incomming_arrow(position, speed));
            }
        }

        void lifetime() {
            timeleft -= Time.deltaTime;

            if (timeleft <= 0)
                virtus.return_();
        }

        [inked]
        public class spectre : Code.spectre {
            [link]
            arrow arrow;

            skin skin;

            public class ink : ink<spectre> {
                public ink(skin skin) {
                    o.skin = skin;
                }
            }

            protected override void _step() {
                Graphics.DrawMesh(skin.mesh, arrow.position, arrow.rotation.applied_after(skin.roty), skin.material, 0);
            }
        }

        [Serializable]
        public struct skin {
            public Vector3 roty;
            public Mesh mesh;
            public Material material;
        }
    }

    public struct perce {
        public float raw;
        public Vector3 position;

        public perce(float _raw, Vector3 _position) {
            raw = _raw;
            position = _position;
        }
    }

    public struct incomming_arrow {
        public Vector3 position;
        public float speed;

        public incomming_arrow(Vector3 position, float speed) {
            this.position = position;
            this.speed = speed;
        }
    }
}