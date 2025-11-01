using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public partial class scr {
        public static void add_mecha ( sb sb ) {
            sb.a < react_explosion > ();
        }
    }

    [path ("mecha")]
    public class react_explosion : action {

        [link]
        health health;

        circuit circuit;

        mecha_explosion explosion;

        protected override void _ready() {
            phoenix.core.start_action (this);
            explosion = with ( new mecha_explosion () );
            circuit = ( circuit ) health.primary;
        }

        protected override void _step() {
            if ( circuit.damaged && !explosion.on )
                phoenix.core.start_action ( explosion );    
        }
    }

    public class mecha_explosion : action {
        [link]
        character c;

        illusion.w explosion;

        const float duration = .5f;
        float t;

        protected override void _ready() {
            explosion = res.illusions.q ( sp.explosion ).get_w ();
        }

        protected override void _start() {
            explosion.fire ( c.position );
            t = duration;
        }
        protected override void _step() {
            if ( t <= 0 ) {
                system.destroy ();
                return;
            }

            t -= Time.deltaTime;
        }
    }
}
