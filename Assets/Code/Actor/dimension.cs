using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class dimension : moon
    {
        [link]
        public character c;

        public float h;
        public float r;

        public Vector3 position => c.position;
    }

    public static class warriorExtension
    {
        public static dimension get_dimension ( this warrior w ) => w.system.get <dimension> ();
    }
}