using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "---", menuName = "RPG/Stellar")]
    public class StellarAuthor : VirtusCreator {
        public Material Material;
        public int FrameNumber;
        public bool Loop;

        stellar.w w;

        protected override void _virtus_create() {
        new stellar.ink ( Material, FrameNumber, Loop );
        }

        public stellar.w get_w () => get_bridge (ref w);
    }
}