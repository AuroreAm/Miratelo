using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UIElements;

namespace Triheroes.Code
{
    [inked]
    public class illusion : spectre
    {
        int illusion_id;
        new ParticleSystem system;

        public class ink : ink < illusion >
        {
            public ink ( ParticleSystem system )
            {
                o.system = system;
            }
        }

        static Vector3 _position;
        public static int fire ( int name, Vector3 position )
        {
            _position = position;
            return orion.rent (name);
        }

        public static void stop ( int name, int illusion_id )
        {
            orion.get <illusion> (name, illusion_id).stop ();
        }

        protected override void __ready()
        {
            system.gameObject.SetActive (false);
        }

        protected override void _start()
        {
            system.gameObject.transform.position = _position;
            system.gameObject.SetActive (true);
        }

        protected override void _stop()
        {
            system.gameObject.SetActive (false);
        }

        void stop ()
        {
            virtus.return_ ();
        }
    }
}
