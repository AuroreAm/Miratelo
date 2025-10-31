using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class mecha_eye : moon
    {
        public Vector3 position => eye.position;
        Transform eye;

        public class ink : ink <mecha_eye> {
            public ink ( Transform eye ) {
                o.eye = eye;
            }
        }
    }
}
