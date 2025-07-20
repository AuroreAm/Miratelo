using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class t_SS2 : thought.final
    {

        public int ComboId;

        [Category ("actor")]
        public class slash_SS2 : package.o <t_SS2>
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
