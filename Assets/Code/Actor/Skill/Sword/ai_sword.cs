using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using static Pixify.treeBuilder;

namespace Triheroes.Code
{
    public class ai_slash_consecutive : reflection
    {
        [Depend]
        m_equip me;

        sequence slash_combo;

        public override void Main()
        {
            TreeStart ( me.character );
            new sequence () { repeat = true };
                new ac_slash () { ComboId = 0 };
                new ac_slash () { ComboId = 1 };
                new ac_slash () { ComboId = 2 };
            end ();
            slash_combo = TreeFinalize () as sequence;
        }

        protected override void OnAquire()
        {
            mst.SetState ( slash_combo, Pri.Action );
        }
    }
}