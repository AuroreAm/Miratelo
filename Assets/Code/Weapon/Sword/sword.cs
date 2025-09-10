using Lyra;
using System.Collections.Generic;
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
        public term slash { get; private set; }
        public term slash_hook_up { get; private set;}
        public term slash_hook_spam { get; private set;}

        public class ink : ink < sword >
        {
            public ink ( float length, string slash_name )
            {
                o.length = length;
                o.slash = new term ( slash_name );
                o.slash_hook_up = new term ( slash_name + "_hook_up" );
                o.slash_hook_spam = new term ( slash_name + "_hook_spam" );
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
