using Lyra;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    public class MechaSkinWriter : SkinWriterModule {
        public Transform EyePosition;

        protected override void _create() {
            new ink <react_explosion> ();
            new mecha_eye.ink ( EyePosition );
        }
    }
}