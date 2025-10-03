using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class PlasmaDisolveAuthor : AuthorModule {
        public override void _create() {
            new ink <plasma_disolve> ();
        }
    }
}
