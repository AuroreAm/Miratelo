using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SwordAuthor : WeaponAuthor
    {
        public float Length;
        public string SlashName;

        protected override weapon __creation()
        {
            return new sword.ink ( Length, SlashName ).o;
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
