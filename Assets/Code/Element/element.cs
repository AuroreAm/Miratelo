using System.Collections;
using System.Collections.Generic;
using Lyra;
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
        matter_registry registry;

        public abstract float damage_factor {get;}
        
        public float mu {get; private set;}

        protected sealed override void _ready() {
            mu = _mu;
        }
    }

    public sealed class air : matter {
        public override float damage_factor => .001f;
    }

    public sealed class titanium : matter {
        public override float damage_factor => 1.25f;
    }

    public sealed class metal : matter {
        public override float damage_factor => 1;
    }

    public sealed class wood : matter {
        public override float damage_factor => .06f;
    }

    [path ("element")]
    public abstract class element : public_moon <element>
    {
        [link]
        public photon photon;

        [link]
        public warrior warrior;
    }
}