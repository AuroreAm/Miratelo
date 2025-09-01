using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class gf_debug : pix
    {
        static gf_debug o;
        Text text;

        public override void Create()
        {
            o = this;
        }

        public class package : PreBlock.Package <gf_debug>
        {
            public package ( Text t )
            {
                o.text = t;
            }
        }

        public static void SetText ( string text )
        {
            o.text.text = text;
        }
    }
}