using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Sword;
using UnityEngine;
using static Triheroes.Code.Sword.Combat.slay;

namespace Triheroes.Code
{
    public class knocker : attack, gold<hacked>
    {
        [link]
        slash slash;

        Vector3 dir;
        float speed;

        static Vector3 _dir;
        static float _speed;

        public static void fire ( int name, sword sword, path path, float duration, Vector3 knock_dir, float knock_speed )
        {
            _dir = knock_dir;
            _speed = knock_speed;
            slash.fire ( name, sword, path, duration );
        }

        protected override void _start()
        {
            dir = _dir;
            speed = _speed;
        }

        public void _radiate(hacked gleam)
        {
            pallas.radiate ( gleam.hacked_id, new knock( dir, speed ) );
        }
    }

    public struct knock
    {
        public Vector3 dir;
        public float speed;

        public knock ( Vector3 _dir, float _speed )
        {
            dir = _dir;
            speed = _speed;
        }
    }
}
