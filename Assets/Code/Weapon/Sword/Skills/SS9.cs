using Lyra;

namespace Triheroes.Code
{
    public class SS9 : moon
    {
        public slay_combo skill;

        static readonly term[] key = {  anim.SS9_0, anim.SS9_1, anim.SS9_2, anim.SS9_1, anim.SS9_2, anim.SS9_1, anim.SS9_2 };

        
        protected override void _ready()
        {
            var combo = new act [key.Length];
             for (int i = 0; i < key.Length; i++)
            combo [i] = with ( new parry_arrow ( key [i] ) );

            skill = with ( new slay_combo ( combo ) );
        }
    }
}