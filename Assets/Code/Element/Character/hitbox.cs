using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class hitbox_registry : moon {
        List <hitbox> hitboxes = new List<hitbox> ();

        public void add ( hitbox hitbox ) {
            hitboxes.Add (hitbox);
        }

        public void disable () {
            foreach ( var h in hitboxes ) {
                h.collider.gameObject.layer = 0;
            }
        }

        public void enable () {
            foreach ( var h in hitboxes ) {
                h.collider.gameObject.layer = vecteur.HITBOX;
            }
        }
    }

    public class hitbox : public_moon <hitbox> {
        [link]
        health health;

        [link]
        public warrior warrior;

        [link]
        character c;

        [link]
        hitbox_registry registry;

        public Collider collider {get; private set;}

        public int character_id => c.gameobject.GetInstanceID ();
        int instance_id;

        public hitbox ( Collider _collider ) {
            collider = _collider;
            collider.gameObject.layer = vecteur.HITBOX;
            instance_id = _collider.GetInstanceID ();
        }

        protected sealed override void _ready() {
            register ( instance_id );
            registry.add ( this );
        }

        public void damage ( damage damage ) {
            health.damage ( damage );
        }
    }
}
