using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class mecha_sword : moon
    {
        [link]
        warrior warrior;
        sword sword;

        public static explicit operator sword ( mecha_sword m )
        {
            return m.sword;
        }

        protected override void _ready()
        {
            sword.handle.aquire ( warrior );
        }

        public class ink : ink < mecha_sword >
        {
            public ink (weapon mecha_sword)
            {
                o.sword = mecha_sword as sword;
            } 
        }
    
    }
}