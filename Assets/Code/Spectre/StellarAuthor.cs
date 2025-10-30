using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "---", menuName = "RPG/Stellar")]
    public class StellarAuthor : VirtusCreator <stellar.w> {
        public Material Material;
        public int FrameNumber;
        public bool Loop;

        protected override void _virtus_create() {
        new stellar.ink ( Material, FrameNumber, Loop );
        }
    }
}