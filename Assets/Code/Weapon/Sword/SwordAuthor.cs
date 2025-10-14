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
        public moon_paper <matter> Matter;

        protected override weapon __creation()
        {
            matter m = Matter.write ();
            new matter_registry.ink ( m );
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