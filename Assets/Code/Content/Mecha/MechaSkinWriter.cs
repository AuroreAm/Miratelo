using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class MechaSkinWriter : Writer
    {
        public Transform BusterOrigin;
        public Transform BusterEnd;

        public Sword MechaSword;

        public override void OnWriteBlock()
        {
            new d_mecha_buster.package ( BusterOrigin, BusterEnd );
            new s_mecha_sword.package ( MechaSword );
        }

        public override void RequiredPix(in List<Type> a)
        {
            a.A <d_mecha_buster> ();
            a.A <s_mecha_sword> ();
        }
    }
}
