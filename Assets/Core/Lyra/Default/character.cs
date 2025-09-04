using UnityEngine;

namespace Lyra
{
    [NeedPackage]
    public class character : dat
    {
        public GameObject GameObject { private set; get; }
        public Transform Coord { private set; get; }
        
        public Vector3 Position => Coord.position;

        public class package : Package <character>
        {
            public package ( GameObject go )
            {
                o.GameObject = go;
                o.Coord = go.transform;
            }
        }
    }
}