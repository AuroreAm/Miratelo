using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // character dimensions used by movement modules like m_capsule_character_controller
    public class d_dimension : pix
    {
        [Depend]
        character c;
        public float h {private set; get;}
        public float r {private set; get;}
        public float m {private set; get;}

        public Vector3 position => c.position;
        public Transform Coord => c.Coord;

        public class package : PreBlock.Package <d_dimension>
        {
            public package ( float h, float r, float m )
            {
                o.h = h;
                o.r = r;
                o.m = m;
            }
        }
    }
}