using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public class ArrowAuthor : VirtusAuthor {
        arrow.w w;

        public moon_paper <matter> Matter;
        List<AuthorModule> modules;

        protected override void _virtus_create() {
            modules = new List<AuthorModule>(GetComponents<AuthorModule>());

            new ink<arrow>();
            new matter_registry.ink ( Matter.write () );

            foreach (var a in modules)
                a._create();
        }

        public override void _created(system s) {
            foreach (var a in modules)
                a._created(s);
        }

        public arrow.w get_w() => bridge_cache(ref w);
    }
}
