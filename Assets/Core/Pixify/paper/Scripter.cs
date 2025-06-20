using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Pixify
{
    public abstract class Scripter : MonoBehaviour
    {
        public virtual ModuleWriter[] GetModules () => new ModuleWriter[] {};
        
        public virtual void OnSpawn ( Vector3 position, Quaternion rotation, Character c ) {}
        public virtual void OnWrite ( Character c ) {}
    }
}