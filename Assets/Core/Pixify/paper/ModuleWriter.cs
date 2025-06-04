using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    // inject module into character
    [Category("")]
    [Serializable]
    public class ModuleWriter
    {
        public virtual void WriteModule (Character character)
        {
        }
    }
}