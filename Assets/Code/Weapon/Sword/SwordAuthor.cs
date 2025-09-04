using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SwordAuthor : MonoBehaviour, IAuthor
    {
        public float Length;
        public string SlashName;

        public void writings()
        {
            var go = Instantiate ( gameObject );
            go.name = gameObject.name;

            new character.package ( go );
            new d_sword.package ( Length, SlashName );
        }

        public d_sword Get ()
        {
            return new shard.constelation.write ( this ).constelation ().get <d_sword> ();
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
