using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class InterfaceAuthor : Author {

        public RawImage FullOverlay;

        public override void _create() {
            new ink <ui> ();
            new full_overlay.ink ( FullOverlay );
        }

        public override void _created(system s) {}
    }
}
