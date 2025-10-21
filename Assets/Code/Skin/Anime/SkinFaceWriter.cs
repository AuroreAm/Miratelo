using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SkinFaceWriter : SkinWriterModule {
        public Material Face;
        public Vector2Int Tiles;

        protected override void _create() {
            new face_animation.ink ( Face, Tiles );
        }
    }
}