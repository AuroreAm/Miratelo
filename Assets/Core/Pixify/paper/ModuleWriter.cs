using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    // TODO: add dependance order description
    // inject module into character
    [Serializable]
    public class ModuleWriter
    {
        public virtual void WriteModule (Character character)
        {
        }
    }
}