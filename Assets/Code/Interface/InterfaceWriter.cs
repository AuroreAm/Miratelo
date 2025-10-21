using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class InterfaceWriter : Writer {

        public RawImage FullOverlay;
        public UILayer l0;
        public UILayer l1;

        protected override void __create() {
            new ui.ink ( GetComponent <Canvas> ().GetComponent <RectTransform> () );
            new full_overlay.ink ( FullOverlay );
            new health_hud.ink ( l0, l1 );
        }
    }
}
