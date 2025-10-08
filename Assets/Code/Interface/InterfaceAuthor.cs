using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class InterfaceAuthor : Author {

        public RawImage FullOverlay;

        public Text Label;
        public RectTransform HeartContainer;
        public RectTransform HeartPrefab;
        public float HeartSize;

        public override void _create() {
            new ui.ink ( Label, HeartContainer, HeartPrefab, HeartSize );
            new full_overlay.ink ( FullOverlay );
        }

        public override void _created(system s) {}
    }
}
