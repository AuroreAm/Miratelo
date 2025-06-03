using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [Serializable]
    [CreateAssetMenu(menuName = "Pixify/Script")]
    public class ScriptModel : ScriptableObject
    {
        [HideInInspector]
        [SerializeReference]
        public ActionModel Root;
    }
}