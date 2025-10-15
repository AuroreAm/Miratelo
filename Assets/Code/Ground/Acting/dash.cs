using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.Acting {

    [path ("acting")]
    public class dash : acting.first {
        Code.dash dash_act;

        [export]
        public direction direction;

        protected override void __ready() {
            dash_act = with ( new Code.dash()._ (direction) );
        }

        protected override act get_act() => dash_act;
    }
}