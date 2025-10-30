using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "---", menuName = "RPG/AfterImage")]
    public class AfterImageAuthor : VirtusCreator <after_image.w> {
        public Material Material;

        protected override void _virtus_create() {
            new after_image.ink ( Material );
        }
    }
}