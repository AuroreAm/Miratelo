using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{

    public class character : pix
    {
        public GameObject gameObject {private set; get;}
        public Transform Coord {private set; get;}
        public Vector3 position => Coord.position;

        public class package : PreBlock.Package <character>
        {
            public package ( GameObject g )
            {     
            o.gameObject = g;
            o.Coord = g.transform;
            }
        }
    }

}