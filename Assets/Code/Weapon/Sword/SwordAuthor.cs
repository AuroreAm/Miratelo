using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SwordAuthor : WeaponAuthor
    {
        public float Length;
        public SlashAuthor Slash;
        public moon_paper < matter > Matter;
        public Rigidbody RigidBody;

        protected override weapon __create ()
        {
            matter m = Matter.write ();
            new matter_registry.ink ( m );

            new sword_rb.ink ( Instantiate (RigidBody) );
            new sword.ink ( Length, Slash.get_w () );

            return new sword.ink ( Length, Slash.get_w () ).o;
        }

        #if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine ( transform.position, transform.position + transform.TransformDirection ( Length * Vector3.forward ) );
        }
        #endif
    }
}