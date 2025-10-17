using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public class StartScriptAuthor : ActorAuthorModule {
        public string StartScript;

        public override void _create() {
            new behavior.ink ( new term (StartScript) );
        }
    }
}
