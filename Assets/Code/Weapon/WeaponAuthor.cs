using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class WeaponAuthor : CharacterAuthor, creator {
        public GameObject WeaponGO;

        weapon weapon;

        public override void Spawn () => SpawnWeapon ();
        
        public weapon SpawnWeapon () {
            new system.creator (this).create_system ();
            return weapon;
        }

        public void _create() {
            GameObject go = Instantiate ( WeaponGO );
            go.transform.SetPositionAndRotation ( transform.position, transform.rotation );

            new character.ink ( go );
            new graphic.ink (go);

            weapon = __create ();
        }

        public void _created(system s) {
            __created (s);
        }
        
        protected abstract weapon __create ();
        protected virtual void __created ( system s ) {}
    }

    /*
    public abstract class WeaponAuthor : MonoBehaviour, creator
    {
        public void _create ()
        {
            new character.ink ( gameObject );
            weapon = __creation ();
        }

        protected abstract weapon __creation ();

        weapon weapon;
        public weapon create_instance ()
        {
            var a = Instantiate (this) ;
            new system.creator ( a ).create_system ();
            return a.weapon;
        }

        public weapon get ()
        {
            new system.creator ( this ).create_system ();
            return weapon;
        }

        public void Start ()
        {
            Destroy (this);
        }

        public virtual void _created(system s)
        {}
    }*/
}