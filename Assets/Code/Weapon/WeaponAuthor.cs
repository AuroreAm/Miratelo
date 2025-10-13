using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
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
    }
}