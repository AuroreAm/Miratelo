using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Pixify
{
    // spawn a character at the location
    public sealed class CharacterAuthor : MonoBehaviour
    {

        public void Start()
        {
            Spawn ( transform.position, transform.rotation );
            Destroy (gameObject);
        }

        public Character Spawn ( Vector3 position, Quaternion rotation )
        {
            var Scripters = GetComponents<Scripter>();
            var c = new GameObject (gameObject.name).AddComponent<Character> ();

            foreach (var a in Scripters)
            foreach (var m in a.GetModules ())
                m.WriteModule (c);

            c.transform.position = position;

            foreach (var a in Scripters)
            a.OnSpawn ( position, rotation, c );

            return c;
        }
    }
}