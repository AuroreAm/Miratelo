using UnityEngine;

namespace Lyra
{
    [inkedPackage]
    public class character : moon
    {
        public GameObject gameobject { private set; get; }
        public Transform coord { private set; get; }
        
        public Vector3 position => coord.position;

        public class ink : ink <character>
        {
            public ink ( GameObject go )
            {
                o.gameobject = go;
                o.coord = go.transform;
            }
        }
    }
}