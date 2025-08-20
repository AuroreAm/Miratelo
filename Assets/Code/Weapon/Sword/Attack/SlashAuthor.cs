using System;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "Slash", menuName = "RPG/SlashModel")]
    public class SlashAuthor : VirtusAuthor
    {
        public Material TrailMaterial;
        public int FrameNumber = 1;

        [SerializeField]
        bool Hooker;

        public override void OnWriteBlock()
        {
            new a_slash_attack.package ( TrailMaterial, FrameNumber );
        }

        protected override void RequiredPix(in List<Type> a)
        {
            a.A<a_slash_attack> ();
            if (Hooker) a.A<a_hook_attack> ();
        }
    }
}
