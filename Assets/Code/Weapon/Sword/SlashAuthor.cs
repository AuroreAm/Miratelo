using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "Slash", menuName = "RPG/SlashModel")]
    public class SlashAuthor : VirtusCreator <slash.w>
    {
        public Material TrailMaterial;
        public int FrameNumber = 1;

        protected override void _virtus_create() {
            new slash.ink ( TrailMaterial, FrameNumber );
        }
    }
}
