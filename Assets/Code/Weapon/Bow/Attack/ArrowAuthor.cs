using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "Slash", menuName = "RPG/ArrowModel")]
    public class ArrowAuthor : VirtusCreator
    {
        public arrow.skin skin;

        protected override void _virtus_creation()
        {
            new arrow.ink ( skin );
        }
    }
}
