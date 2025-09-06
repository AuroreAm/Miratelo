using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class hands : moon
    {
        Transform[] hand_transforms;

        public static explicit operator Transform[] (hands o)
        {
            return o.hand_transforms;
        }

        public class ink : ink<hands>
        {
            public ink (Transform[] hands)
            {
                o.hand_transforms = hands;
            }
        }
    }
}
