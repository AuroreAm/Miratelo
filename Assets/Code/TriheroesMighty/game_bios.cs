using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [superstar]
    public class morai : bios
    {
        bios current;

        protected override void _ready()
        {
            phoenix.core.execute (this);
        }

        public void change ( bios bios )
        {
            if ( current != null )
            unlink ( current );

            current = bios;
            link ( bios );
        }
    }
}
