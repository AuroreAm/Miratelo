using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Sword;
using UnityEngine;

namespace Triheroes.Code.Element
{
    public class armored_metal : element, pearl <hack>
    {
        [export]
        public float _max_AP;
        [export]
        public float cooldown = 10;
        [export]
        public float restoration_per_second = 0.1f;

        [link]
        restorer restore;

        float AP;

        protected override void _ready()
        {
            register ( system.get <character> ().gameobject.GetInstanceID () );

            AP = _max_AP;
            phoenix.core.start ( restore );
        }

        public void _radiate(hack gleam)
        {
            if (AP > 0)
            AP -= gleam.raw;
            else
            photon.radiate ( new damage (gleam.raw) );

            restore.cooldown = cooldown;
        }

        class restorer : controller
        {
            [link]
            armored_metal o;

            public float cooldown;

            protected override void _step()
            {
                if (cooldown > 0)
                    cooldown -= Time.deltaTime;
                else if (o.AP != o._max_AP)
                    o.AP = Mathf.MoveTowards(o.AP, o._max_AP, o.restoration_per_second * Time.deltaTime); 
            }
        }
    }
}
