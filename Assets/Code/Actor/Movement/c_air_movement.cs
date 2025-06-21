using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [RegisterAsBase]
    public class c_air_movement : controller
    {
        [Depend]
        public m_capsule_character_controller mccc;
        [Depend]
        public m_ground_data mgd;
        [Depend]
        public m_skin ms;

        protected override void OnAquire()
        {
            ms.PlayState ( 0, AnimationKey.fall, 0.1f );
            mccc.Aquire(this);
        }

        public void AirMove(Vector3 DirPerSecond,float WalkFactor = WalkFactor.run)
        {
            if (on)
            mccc.dir += DirPerSecond * Time.deltaTime * WalkFactor;
        }

        public override void Main()
        {}

        protected override void OnFree()
        {
            mccc.Free(this);
        }
    }
}
