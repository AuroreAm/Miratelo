using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public class health : controller {
        public health_system primary {private set; get;}

        protected override void _ready() {
            phoenix.core.execute (this);
        }

        public void damage (float raw) {
            primary.damage (raw);
        }

        public void put_primary (health_system hs) {
            if (primary != null) {
                unlink ( primary );
            }

            primary = hs;
            link ( hs );
        }
    }

    public abstract class health_system : auto_stat {
        public abstract void damage (float raw);
    }
}