using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class MechaBusterSkinWriter : Writer
    {

        public Transform BusterOrigin;
        public Transform BusterEnd;

        public override void OnWriteBlock()
        {
            new d_mecha_buster.package ( BusterOrigin, BusterEnd );
        }

        public override void RequiredPix(in List<Type> a)
        {
            a.A <d_mecha_buster> ();
        }
    }
}
