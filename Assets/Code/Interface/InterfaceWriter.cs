using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class InterfaceWriter : Writer {

        public RawImage FullOverlay;
        public RectTransform [] Healths;
        public RectTransform Energy;
        public Text Prompt;

        protected override void __create() {
            new ui.ink ( GetComponent <Canvas> ().GetComponent <RectTransform> () );
            new full_overlay.ink ( FullOverlay );
            new player_hud.ink ( Healths, Energy, Prompt );
        }
    }
}