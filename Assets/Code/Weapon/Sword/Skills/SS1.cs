using Lyra;

namespace Triheroes.Code
{
    public class SS1 : skill {
        [link]
        stamina stamina;

        slay_combo combo;
        
        static readonly term[] key = { anim.SS1_0, anim.SS1_1, anim.SS1_2 };

        protected override void _ready() {
            var combo = new act [3];
             for (int i = 0; i < 3; i++)
            combo [i] = with ( new slay ( key [i] ) );

            this.combo = with ( new slay_combo( combo ) );
        }

        public void spam () {
            if ( stamina.has_green () )
            combo.spam ();
        }
    }
}