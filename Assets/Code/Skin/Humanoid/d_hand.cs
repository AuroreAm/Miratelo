using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [NeedPackage]
    public class d_hand : dat
    {
        public Transform[] Hands { private set; get; }

        public class package : Package<d_hand>
        {
            public package(Transform[] hands)
            {
                o.Hands = hands;
            }
        }
    }
}
