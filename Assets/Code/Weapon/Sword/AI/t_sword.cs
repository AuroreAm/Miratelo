using System.Collections;
using System.Collections.Generic;
using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class t_SS1 : thought.final
    {
        public int ComboId;

        [Category ("actor")]
        public class slash_SS1 : package.o <t_SS1>
        {
            [Export]
            public int ComboId;

            protected override void BeforeStart()
            {
                main.ComboId = ComboId;
            }
        }
    }
}
