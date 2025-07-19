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
        public class draw_from_inv_0 : thought
        {
            [Depend]
            t_draw_from_inv_0 main;

            [Export]
            public int index;
            [Export]
            public WeaponType weaponType;

            protected override void OnAquire()
            {
                main.index = index;
                main.WeaponType = weaponType;
                main.Aquire (this);
            }
        }
    }

    public class t_return_from_se_to_inv_0 : thought.final
    {
        [Category ("actor")]
        public class return_from_se_to_inv_0 : thought
        {
            [Depend]
            t_return_from_se_to_inv_0 main;

            protected override void OnAquire()
            {
                main.Aquire (this);
            }
        }
    }

}
