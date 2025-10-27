using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class sword_rb : weapon_handle.free <sword> {

        [link]
        character c;

        Rigidbody rb;
        public class ink : ink <sword_rb> {
            public ink ( Rigidbody rb ) {
                o.rb = rb;
            }
        }

        protected override void __start() {
            rb.gameObject.SetActive ( true );
            rb.transform.position = c.position;
            rb.transform.rotation = c.coord.rotation;
        }

        protected override void _step() {
            c.coord.position = rb.transform.position;
            c.coord.rotation = rb.transform.rotation;
        }

        protected override void _stop() {
            rb.gameObject.SetActive ( false );
        }

    }
}