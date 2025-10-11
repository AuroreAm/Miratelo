using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class HumanStatAuthor : ActorAuthorModule {
        public int Heart = 1;
        public int Stamina = 7;

        public override void _create() {
            new ink <health> ();
        }

        public override void _created(system s) {
            var l = new life ( Heart );
            s.add ( l );
            s.get <health> ().put_primary (l);
        }
    }
}