using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class d_dimension_meta : dat
    {
        [Link]
        public character Character;

        public float Height;
        public float Radius;

        public Vector3 Position => Character.Position;
    }
}