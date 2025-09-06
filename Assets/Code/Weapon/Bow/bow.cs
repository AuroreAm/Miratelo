using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    [inked]
    public class bow : weapon
    {
        Transform bowstring;
        public term arrow;

        public class ink : ink < bow >
        {
            public ink ( Transform bowstring, string arrow )
            {
                o.bowstring = bowstring;
                o.arrow = new term ( arrow );
            }
        }

        public Vector3 rot => vecteur.rot_direction ( bowstring.position, coord.position );
        public Vector3 string_position => bowstring.position;
    }
}