using Lyra;
using System.Collections.Generic;
using Triheroes.Code.Sword;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class sword : weapon
    {
        [link]
        character c;
        public Vector3 position => c.position;

        public float length { get; private set; }
        public slash.w slash { get; private set; }

        public class ink : ink < sword >
        {
            public ink ( float length, slash.w slash )
            {
                o.length = length;
                o.slash = slash;
            }
        }

        public void enable_parry ()
        {
            c.gameobject.layer = vecteur.ATTACK;
        }

        public void disable_parry ()
        {
            c.gameobject.layer = 0;
        }

    }
}
