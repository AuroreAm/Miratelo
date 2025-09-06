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


        protected override void _virtus_creation()
        {
            new Sword.slash.ink ( TrailMaterial, FrameNumber );
        }
    }
}
