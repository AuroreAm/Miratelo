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

        static Dictionary < int, illusion > _active_illusion = new Dictionary<int, illusion> ();
        static Vector3 _position;
        static int static_counter;
        public static int fire ( int name, Vector3 position )
        {
            static_counter ++;
            _position = position;
            orion.rent (name);
            return static_counter;
        }

        public static void stop ( int illusion_id )
        {
            _active_illusion [illusion_id].stop ();
        }

        protected override void __ready()
        {
            system.gameObject.SetActive (false);
        }

        protected override void _start()
        {
            illusion_id = static_counter;
            _active_illusion.Add ( static_counter, this );

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
