using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SwordAuthor : MonoBehaviour, creator
    {
        public float Length;
        public string SlashName;

        public void _creation()
        {
            var go = Instantiate ( gameObject );
            go.name = gameObject.name;

            new character.ink ( go );
            new sword.ink ( Length, SlashName );
        }

        public sword Get ()
        {
            return new system.creator ( this ).create_system ().get <sword> ();
        }

        void Start ()
        {
            Destroy ( this );
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
