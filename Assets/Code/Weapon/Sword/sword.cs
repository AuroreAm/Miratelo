using Lyra;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class sword : weapon
    {
        [link]
        character c;
        public Vector3 position => c.position;

        public float length { get; private set; }
        public term slash { get; private set; }

        public class ink : ink < sword >
        {
            public ink ( float length, string slash_name )
            {
                o.length = length;
                o.slash = new term ( slash_name );
            }
        }

    }
}
