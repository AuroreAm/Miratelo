using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    public class S1_charge : act {        
        public override priority priority => priority.action;

        [link]
        skin skin;

        [link]
        stand stand;

        protected override void _start() {
            stand.use (this);

            skin.play ( new skin.animation ( animation.S1_charge, this ) );
        }

        protected override void _step() {
            stand.rotate_skin ();
        }
    }

    class S1 : act {
        public override priority priority => priority.action;
        
        [link]
        skin skin;

        [link]
        axeal a;

        force_curve_data f;
        
        const float duration = 2;
        float time;

        protected override void _ready() {
            f = new force_curve_data ( duration, res.curves.q (animation.jump) );
        }

        protected override void _start() {
            time = 0;

            skin.play ( new skin.animation ( animation.S1, this )  );

            f.dir = vecteur.ldir ( skin.roty, Vector3.forward ) * 20;
            a.set_force (f);
        }

        protected override void _step() {
            if ( time >= duration )
            stop ();

            time += Time.deltaTime;
        }
    }
}