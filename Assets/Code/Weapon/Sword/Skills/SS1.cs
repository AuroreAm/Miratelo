using Lyra;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Triheroes.Code.Sword.Combat;

namespace Triheroes.Code
{
    public class SS1 : moon
    {
        public slay_combo skill { private set; get; }

        static readonly term[] key = { animation.SS1_0, animation.SS1_1, animation.SS1_2 };

        protected override void _ready()
        {
            var combo = new act [3];
             for (int i = 0; i < 3; i++)
            combo [i] = new slay ( key [i] );

            skill = new slay_combo ( combo );
        }
    }
}
