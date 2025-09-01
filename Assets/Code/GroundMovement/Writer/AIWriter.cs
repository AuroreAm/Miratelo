using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class AIWriter : Writer
    {
        public PixPaper<cortex> MainCortex;
        public RolePlay AI;

        public override void OnWriteBlock()
        {
        }

        public override void RequiredPix(in List<Type> a)
        {
            a.A <s_mind> ();
        }

        public override void AfterSpawn(Vector3 position, Quaternion rotation, block b)
        {
            b.GetPix <s_mind> ().SetCortex ( MainCortex.Write () );
            var AIBehaviors = AI.GetThoughtConcepts (b);
            b.GetPix <s_mind> ().AddConcepts ( AIBehaviors );
            b.GetPix <s_mind> ().master.StartRootThought ( AIBehaviors [0].Item2 );
        }
    }
}
