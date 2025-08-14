using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public abstract class Writer : MonoBehaviour
    {
        public abstract void OnWriteBlock ();
        public abstract void RequiredPix ( in List <Type> a );

        public virtual void AfterSpawn ( Vector3 position, Quaternion rotation, block b )
        {}

        public virtual void AfterWrite ( block b )
        {}

        public static Type Q<T> () => typeof (T);
    }
}