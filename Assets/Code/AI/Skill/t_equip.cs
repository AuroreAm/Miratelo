using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class t_draw_from_inv_0 : thought.final
    {
        public int index;
        public WeaponType WeaponType;

        [Category ("actor")]
        public class draw_from_inv_0 : package.o <t_draw_from_inv_0>
        {
            [Export]
            public int index;
            [Export]
            public WeaponType weaponType;

            protected override void BeforeStart()
            {
                main.index = index;
                main.WeaponType = weaponType;
            }
        }
    }

    public class t_return_from_se_to_inv_0 : thought.final
    {
        [Category ("actor")]
        public class return_from_se_to_inv_0 : package.o <t_return_from_se_to_inv_0>
        {}
    }

}
