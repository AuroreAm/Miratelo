using Lyra;

namespace Triheroes.Code
{
    public class SS9 : moon
    {
        public slay_combo skill;

        static readonly term[] key = {  animation.SS9_0, animation.SS9_1, animation.SS9_2, animation.SS9_1, animation.SS9_2, animation.SS9_1, animation.SS9_2 };

        
        protected override void _ready()
        {
            var combo = new act [key.Length];
             for (int i = 0; i < key.Length; i++)
            combo [i] = new parry_arrow ( key [i] );

            skill = new slay_combo ( combo );
        }
    }
}