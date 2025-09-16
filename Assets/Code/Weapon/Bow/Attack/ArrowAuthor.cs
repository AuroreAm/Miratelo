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
        public bool arrow_billboard;

        protected override void _virtus_create()
        {
            if ( arrow_billboard )
            new arrow_billboard.ink ( skin );
            else
            new arrow.spectre.ink ( skin );
        }
    }
}
