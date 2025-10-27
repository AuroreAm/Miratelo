using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "---", menuName = "RPG/AfterImage")]
    public class AfterImageAuthor : VirtusCreator {
        public Material Material;

        protected override void _virtus_create() {
            new after_image.ink ( Material );
        }

        after_image.w w;
        public after_image.w get_w () => bridge_cache (ref w);
    }
}