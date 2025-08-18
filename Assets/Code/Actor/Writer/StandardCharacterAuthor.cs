using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    [RequireComponent(typeof(ActorWriter))]
    [RequireComponent(typeof(SkinWriter))]
    public class StandardCharacterAuthor : Writer
    {
        public override void RequiredPix(in List<Type> a)
        {
            a.A <s_skin> ();
        }

        override public void AfterSpawn(Vector3 position, Quaternion rotation, block b)
        {
            b.GetPix <s_skin> ().rotY = rotation.eulerAngles.y;
        }

        public override void OnWriteBlock()
        {
        }
    }
}