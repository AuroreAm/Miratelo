using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    [inked]
    public class d_actor : shard
    {
        [harmony]
        character c; 

        public string Name { private set; get; }

        protected override void harmony()
        {
            c.form.layer = Vecteur.CHARACTER;
        }

        public class package : ink <d_actor>
        {
            public package ( string name )
            {
                o.Name = name;
            }
        }

        public static implicit operator bool(d_actor exists)
        {
            if (exists != null)
            return exists.c.form;
            else
            return false;
        }
    }
}