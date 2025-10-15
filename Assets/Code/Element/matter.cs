using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public sealed class matter_registry : moon {
        public matter matter { private set; get;}

        public class ink : ink <matter_registry> {
            public ink (matter matter) {
                o.matter = matter;
            }
        }

        protected override void _ready() {
            system.add ( matter );
        }
    }

    [path ("element")]
    public abstract class matter : moon {
        [export]
        public float _mu;

        [link]
        photon photon;

        public abstract float damage_factor {get;}
        
        public float mu {get; private set;}

        protected sealed override void _ready() {
            mu = _mu;
            __ready ();
        }
        protected virtual void __ready () {}

        public damage reaction ( damage damage ) {
            push ( damage );
            return _reaction ( damage );
        }

        protected virtual damage _reaction ( damage damage ) { return damage; }

        public const float vu_to_dir_conversion = 250;
        public void push ( damage damage ) {
            Vector3 dir_per_mu =  damage.vu * damage.damager.mu * damage.normal * vu_to_dir_conversion;
            photon.radiate ( new push ( dir_per_mu ) );
        }
    }

    public sealed class air : matter {
        public override float damage_factor => .001f;
    }


    public sealed class wood : matter {
        public override float damage_factor => .06f;
    }
}