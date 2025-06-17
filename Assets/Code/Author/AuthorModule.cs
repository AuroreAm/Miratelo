using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    [RequireComponent(typeof(CharacterAuthor))]
    public abstract class AuthorModule : MonoBehaviour
    {
        public abstract ModuleWriter[] GetModules ();
        
        public virtual void OnSpawn ( Vector3 position, Quaternion rotation, Character c ) {}
    }
}