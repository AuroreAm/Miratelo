using Lyra;

namespace Triheroes.Code
{
    public class plasma_disolve : attack {
        
        [link]
        arrow arrow;

        stellar.w explosion;

        protected override void __ready() {
            explosion = res.stellars.q (new term ("disolve")).get_w ();
        }

        protected override void _stop() {
            explosion.fire ( arrow.position );
        }
    }
}
