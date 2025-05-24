using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    // TODO: add a default character controller
    [CreateAssetMenu(menuName = "Pixify/Character Model")]
    public class CharacterModel : ScriptableObject
    {
        [SerializeReference]
        public List<ModuleWriter> Parameters;
    }
}