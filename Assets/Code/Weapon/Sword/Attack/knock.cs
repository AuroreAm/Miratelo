using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class knocker : attack, gold<hacked>
    {
        Vector3 dir;
        float speed;

        #region fire
        static Vector3 _dir;
        static float _speed;

        public class w : slash.w {
            public void fire ( sword sword, slay.path path, float duration, Vector3 knock_dir, float knock_speed )
            {   
                _dir = knock_dir.normalized;
                _speed = knock_speed;
                fire (  sword, path, duration );
            }
        }
        #endregion

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
