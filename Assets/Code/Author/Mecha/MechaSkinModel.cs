using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class MechaSkinModel : Writer
    {
        public Transform Spine;
        public Transform ArmBuster;

        public override void OnWriteBlock()
        {
            new sp_mb.package ( Spine, ArmBuster );
        }

        public override void RequiredPix(in List<Type> a)
        {
            a.A <sp_mb> ();
        }
    }
}
