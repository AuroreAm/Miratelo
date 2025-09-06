using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class WeaponAuthor : MonoBehaviour, creator
    {
        public void _creation()
        {
            var go = Instantiate ( gameObject );
            go.name = gameObject.name;

            new character.ink ( go );
            weapon = __creation ();
        }

        protected abstract weapon __creation ();

        weapon weapon;
        public weapon get ()
        {
            new system.creator (this).create_system ();
            return weapon;
        }

        public void Start ()
        {
            Destroy (this);
        }
    }
}