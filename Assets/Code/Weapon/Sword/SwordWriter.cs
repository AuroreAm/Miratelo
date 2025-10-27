using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SwordWriter : WeaponWriter {
        public float Length;
        public SlashAuthor Slash;
        public moon_paper < matter > Matter;
        public GameObject RigidBody;

        protected override void __create() {
            matter m = Matter.write ();
            new character.ink ( gameObject );
            new matter_registry.ink (m);
            new sword_rb.ink ( RigidBody.GetComponent<Rigidbody> () );
            new sword.ink ( Length, Slash.get_w () );
        }
    }
}
