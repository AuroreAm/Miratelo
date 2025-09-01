using System;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class StatWriter : Writer
    {
        [Header("Health")]
        public float MaxHP;

        [Header("Immediate Energy")]
        public float MaxIE = 8;
        public float IEPerSecond = 1;
        public float DelayTime = 2;

        public override void RequiredPix(in List<Type> a)
        {
            if (MaxHP > 0)
            a.A <d_HP> ();
            
            if (MaxIE > 0)
            {
                a.A <d_ie> ();
                a.A <s_ie_metabolism> ();
            }
        }

        public override void AfterWrite(block b)
        {
            if (MaxHP > 0)
            b.GetPix <d_HP> ().Set ( MaxHP );
            
            if (MaxIE > 0)
            {
                b.GetPix <d_ie> ().Set ( MaxIE );
                var sie_generator = b.GetPix <s_ie_metabolism> ();
                sie_generator.IEPerSecond = IEPerSecond;
                sie_generator.DelayTime = DelayTime;
            }
        }

        public override void OnWriteBlock()
        {}
    }
}