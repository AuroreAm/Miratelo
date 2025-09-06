using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    [inked]
    public class actor : moon
    {
        [link]
        character c; 

        [link]
        public skin skin;

        public Vector3 position => c.position;

        public term term;
        public string name { private set; get; }

        protected sealed override void _ready()
        {
            c.gameobject.layer = vecteur.CHARACTER;
        }

        public class ink : ink <actor>
        {
            public ink ( string name )
            {
                o.name = name;
                o.term = new term ( name );
            }
        }

        public static implicit operator bool(actor exists)
        {
            if (exists != null)
            return exists.c.gameobject;
            else
            return false;
        }
    }
}