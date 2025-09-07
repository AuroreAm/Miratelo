using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class MechaSkinAuthor : SkinAuthorModule
    {
        public SwordAuthor MechaSword;
            
        public override void _creation()
        {
            new mecha_sword.ink ( MechaSword.get () );
        }
    }
}
