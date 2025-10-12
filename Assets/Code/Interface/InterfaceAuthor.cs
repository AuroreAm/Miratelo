using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class InterfaceAuthor : Author {

        public RawImage FullOverlay;

        public RectTransform HeartContainer;
        public RectTransform StaminaContainer;

        public override void _create() {
            new ui.ink ( GetComponent <Canvas> ().GetComponent <RectTransform> () );
            new full_overlay.ink ( FullOverlay );
            new player_hud.ink ( HeartContainer, StaminaContainer );
        }

        public override void _created(system s) {
        }
    }
}
