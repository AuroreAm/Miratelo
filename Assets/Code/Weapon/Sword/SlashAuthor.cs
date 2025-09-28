using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Sword;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "Slash", menuName = "RPG/SlashModel")]
    public class SlashAuthor : VirtusCreator
    {
        public Material TrailMaterial;
        public int FrameNumber = 1;
        slash.w w;

        protected override void _virtus_create() {
            new slash.ink ( TrailMaterial, FrameNumber );
        }

        public slash.w get_w () => get_bridge (ref w);
    }
}
