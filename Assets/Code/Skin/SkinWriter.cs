using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SkinWriter : WriterModule
    {
        public float OffsetRotationY;
        public float OffsetPositionY;

        protected override void _create ()
        {
            new graphic.ink ( gameObject );
            new skin.ink ( gameObject, new Vector2 (OffsetRotationY, OffsetPositionY ) );

            var modules = GetComponents<SkinWriterModule>();
            foreach (var a in modules)
            a.Create ();
        }

        protected override void _created (system system)
        {
            var modules = GetComponents<SkinWriterModule>();
            foreach (var a in modules)
            a.Created (system);
        }
    }
}
