using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    public class MechaSkinAuthor : SkinAuthorModule
    {
        public SwordAuthor MechaSword;

        public Transform BusterOrigin;
        public Transform BusterEnd;
            
        public override void _create ()
        {
            new mecha_sword.ink ( MechaSword.get () );
            new mecha_buster.ink ( BusterOrigin, BusterEnd );
        }
    }
}
