using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public static class Act
    {
        public static void Start (action ScriptRoot)
        {
            if (ScriptRoot != null)
            {
                Stage.Start1 ( ScriptRoot );
            }
            else
            Debug.LogWarning ("script is null");
        }
    }
}
