using Lyra;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class metal : matter {
        [export]
        public bool _voltic;
        public bool voltic {get; private set;}

        protected override void __ready() {
        voltic = _voltic;
        }

        protected override damage _reaction(damage damage) {
            if ( damage.damager is metal m ) {
                if ( m.voltic )
                damage.value *= 10;
            }

            nova.fire ( sp.impact_metal, damage.point );

            return damage;
        }
    }

    public sealed class titanium : metal {
        public override float damage_factor => 1.25f;
    }

    public sealed class iron : metal {
        public override float damage_factor => 1;
    }
}
