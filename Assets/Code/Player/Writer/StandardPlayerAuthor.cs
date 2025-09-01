using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using System;
using Lyra.Spirit;

namespace Triheroes.Code
{
    // for players character with humanoid characteristics
    // don't need character controller here, automatically added by game master
    [RequireComponent(typeof(StatWriter))]
    public class StandardPlayerAuthor : StandardCharacterAuthor
    {
        public override void RequiredPix(in List<Type> a)
        {
            base.RequiredPix(a);
            a.A <s_mind> ();
        }
    }
}