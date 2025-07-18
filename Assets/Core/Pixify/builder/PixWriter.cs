using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [Serializable]
    public abstract class PixWriter
    {
        public abstract void RequiredPix (in List <Type> a);
        public virtual void OnWriteBlock () {}
        public virtual void AfterWrite (block b) {}
    }
}