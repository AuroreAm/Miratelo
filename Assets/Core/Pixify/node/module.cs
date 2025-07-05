using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [Serializable]
    public class module : catom
    {
        /// <summary>
        /// is this module with a valid gameobject
        /// </summary>
        public static implicit operator bool(module exists)
        {
            if (exists != null)
            return exists.character;
            else
            return false;
        }
    }
}