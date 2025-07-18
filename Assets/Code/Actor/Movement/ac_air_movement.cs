using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_air_movement : controller
    {
        [Depend]
        public s_capsule_character_controller sccc;
        [Depend]
        public d_ground_data dgd;
        [Depend]
        public s_skin ss;

        protected override void Start()
        {
            ss.PlayState ( 0, AnimationKey.fall, 0.1f );
        }

        public void AirMove(Vector3 DirPerSecond,float WalkFactor = WalkFactor.run)
        {
            if (on)
            sccc.dir += DirPerSecond * Time.deltaTime * WalkFactor;
        }
    }
}
