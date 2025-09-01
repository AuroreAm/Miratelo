using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_mecha_sword : pix
    {
        [Depend]
        d_actor da; 
        public Sword MechaSword { get; private set; }

        public override void Create()
        {
            MechaSword.Aquire ( da );
        }

        public class package : PreBlock.Package < s_mecha_sword >
        {
            public package ( Sword MechaSword ) => o.MechaSword = MechaSword;
        }
    }
}