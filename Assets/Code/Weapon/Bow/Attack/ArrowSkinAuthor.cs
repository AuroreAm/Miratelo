using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public class ArrowSkinAuthor : AuthorModule {

        public arrow.skin skin;
        public bool billboard;

        public override void _create() {
            if ( billboard )
            new arrow_billboard.ink ( skin );
            else
            new arrow.spectre.ink (skin);
        }
    }
}
