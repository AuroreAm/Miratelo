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
            new character.ink ( gameObject );
            weapon = __creation ();
        }

        protected abstract weapon __creation ();

        weapon weapon;
        public weapon get ()
        {
            var a = Instantiate (this) ;
            new system.creator ( a ).create_system ();
            return a.weapon;
        }

        public void Start ()
        {
            Destroy (this);
        }
    }
}