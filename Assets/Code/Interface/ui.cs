using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ui : moon {

        public static ui o;

        [link]
        public full_overlay full_overlay;

        protected override void _ready() {
            o = this;
        }

    }
}
