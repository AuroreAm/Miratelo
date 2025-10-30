using Lyra;
using UnityEngine;

namespace Triheroes.Code.Axeal
{
    [star(order.capsule)]
    [inked]
    public class capsule : star.main {
        public character c => _c;
        [link]
        character _c;

        [link]
        hitbox_registry hitbox;

        public Transform coord { get; private set; }

        public float h { get; private set; }
        public float r { get; private set; }
        public CharacterController cc {get; private set;}
        Vector3 dir;

        public class ink : ink <capsule> {
            public ink (float h, float r) {
                o.h = h;
                o.r = r;
            }
        }

        protected override void _ready() {
            cc = _c.gameobject.AddComponent<CharacterController>();
            cc.skinWidth = 0.0001f;
            cc.height = h;
            cc.radius = r;
            cc.center = new Vector3 (0, h / 2, 0);

            coord = _c.gameobject.transform;
        }

        protected override void _step() {
            hitbox.disable ();
            Physics.IgnoreLayerCollision(coord.gameObject.layer, vecteur.ATTACK, true);
            Physics.IgnoreLayerCollision(coord.gameObject.layer, vecteur.HITBOX, false);
            cc.Move (dir);
            hitbox.enable ();
            Physics.IgnoreLayerCollision(coord.gameObject.layer, vecteur.HITBOX, true);
            Physics.IgnoreLayerCollision(coord.gameObject.layer, vecteur.ATTACK, false);

            dir = Vector3.zero;
        }

        public void move (Vector3 _dir) {
            dir += _dir;
        }
    }
}