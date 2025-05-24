using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // character dimensions used by movement modules like m_capsule_character_controller
    public class m_dimension : module
    {
        public float h;
        public float r;
        public float m;

        public Vector3 position => character.transform.position;

        public m_dimension ( float h, float r, float m )
        {
            this.h = h;
            this.r = r;
            this.m = m;
        }
    }
}