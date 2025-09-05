using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    [inkedPackage]
    public class actor : moon
    {
        [link]
        character c; 

        public string name { private set; get; }

        protected override void _ready()
        {
            c.gameobject.layer = vecteur.CHARACTER;
        }

        public class ink : ink <actor>
        {
            public ink ( string name )
            {
                o.name = name;
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