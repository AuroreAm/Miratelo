using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class GroundAuthor : MonoBehaviour
    {
        public enum GroundType { wood, asphalt }
        public GroundType type;

        void Start()
        {

            Destroy (this);
        }
    }
}
