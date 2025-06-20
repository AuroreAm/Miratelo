using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    // write a character into an already existing gameobject
    public sealed class CharacterWriter : MonoBehaviour
    {
        void Awake()
        {
            var Scripters = GetComponents<Scripter>();
            var c = gameObject.AddComponent<Character> ();

            foreach (var a in Scripters)
            foreach (var m in a.GetModules ())
                m.WriteModule (c);

            
            foreach (var a in Scripters)
            a.OnWrite ( c );

            Destroy (this);
        }
    }
}