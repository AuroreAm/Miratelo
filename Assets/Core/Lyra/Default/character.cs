using UnityEngine;

namespace Lyra
{
    [inked]
    public class character : shard
    {
        public GameObject form { private set; get; }
        public Transform coord { private set; get; }
        
        public Vector3 anchor => coord.position;

        public class package : ink <character>
        {
            public package ( GameObject go )
            {
                o.form = go;
                o.coord = go.transform;
            }
        }
    }
}