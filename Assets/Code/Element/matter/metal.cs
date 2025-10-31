using Lyra;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class metal : matter {
        [export]
        public bool _voltic;
        public bool voltic {get; private set;}

        flash flash;

        protected override void __ready() {
            voltic = _voltic;
            flash = with ( new flash ( res.material.q ( sp.white ) ) );
        }

        protected override damage _reaction(damage damage) {
            if ( damage.damager is metal m ) {
                if ( m.voltic )
                damage.value *= 10;
            }

            nova.fire ( sp.impact_metal, damage.point );
            flash.start ( .01f );
            

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