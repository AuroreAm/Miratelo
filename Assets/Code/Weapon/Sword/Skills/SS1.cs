using Lyra;

namespace Triheroes.Code
{
    public class SS1 : skill {
        [link]
        sword_user sword_user;

        combo_container combo;

        protected override void _ready() {
            combo = with ( new combo_container ( x => new slay (x), anim.SS1_0, anim.SS1_1, anim.SS1_2 ) );
        }

        public void spam () {
            if ( sword_user.on )
            combo.spam ();
        }
    }
}