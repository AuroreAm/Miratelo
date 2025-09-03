using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    [NeedPackage]
    public class d_actor : dat
    {
        [Link]
        character c; 

        public string Name { private set; get; }

        protected override void OnStructured()
        {
            c.GameObject.layer = Vecteur.CHARACTER;
        }

        public class package : Package <d_actor>
        {
            public package ( string name )
            {
                o.Name = name;
            }
        }

        public static implicit operator bool(d_actor exists)
        {
            if (exists != null)
            return exists.c.GameObject;
            else
            return false;
        }
    }
}