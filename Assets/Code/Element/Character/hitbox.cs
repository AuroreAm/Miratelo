using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class hitbox : public_moon <hitbox> {

        [link]
        health health;

        [link]
        public warrior warrior;

        [link]
        character c;

        public int character_id => c.gameobject.GetInstanceID ();
        int instance_id;

        public hitbox ( Collider collider ) {
            instance_id = collider.GetInstanceID ();
        }

        protected sealed override void _ready() {
            register ( instance_id );
        }

        public void damage ( damage damage ) {
            health.damage ( damage );
        }
    }
}
