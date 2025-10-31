using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class graphic : moon {
        
        public GameObject root { private set; get; }

        public class ink : ink < graphic > {
            public ink ( GameObject _root ) {
                o.root = _root;
            }
        }
    }
}
