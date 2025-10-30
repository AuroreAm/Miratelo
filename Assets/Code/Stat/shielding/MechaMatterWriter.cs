using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class MechaMatterWriter : SkinWriterModule {
        public moon_paper <metal> Matter;
        public int ShieldHP;
        public int CircuitHeart;

        circuit c;
        shielding h;
        protected override void _create() {
            c = new circuit ( CircuitHeart );
            h = new shielding ( ShieldHP, Matter.write () );
            new health.ink ( c, h );
        }
    }
}
