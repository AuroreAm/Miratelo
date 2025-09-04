using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inkedAttribute]
    public class d_hand : shard
    {
        public Transform[] Hands { private set; get; }

        public class package : ink<d_hand>
        {
            public package(Transform[] hands)
            {
                o.Hands = hands;
            }
        }
    }
}
