using Lyra;

namespace Triheroes.Code
{
    public class SS9 : skill
    {
        combo_container combo;

        static readonly term[] key = {  anim.SS9_0, anim.SS9_1, anim.SS9_2, anim.SS9_1, anim.SS9_2, anim.SS9_1, anim.SS9_2 };

        
        protected override void _ready()
        {
            combo = with ( new combo_container ( x => new parry_arrow (x), anim.SS9_0, anim.SS9_1, anim.SS9_2 ) );
        }

        public void spam () {
            combo.spam ();
        }
    }
}