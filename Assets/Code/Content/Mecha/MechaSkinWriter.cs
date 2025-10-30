using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    public class MechaSkinWriter : SkinWriterModule {
        protected override void _create() {
            new ink <react_explosion> ();
        }
    }
}
