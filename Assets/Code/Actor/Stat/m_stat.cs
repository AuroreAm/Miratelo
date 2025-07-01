using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Serializable]
    public class stat_writer : ModuleWriter
    {
        [Header("Health")]
        public float MaxHP;

        [Header("Immediate Energy")]
        public float MaxIE = 8;
        public float IEPerSecond = 1;
        public float DelayTime = 2;

        public override void WriteModule(Character character)
        {
            if (MaxHP > 0)
            {
                var mshp = character.RequireModule<m_HP>();
                mshp.Set(MaxHP);
            }

            if (MaxIE > 0)
            {
                var mie = character.RequireModule<m_ie>();
                mie.Set(MaxIE);

                var mie_generator = character.RequireModule<m_ie_metabolism>();
                mie_generator.IEPerSecond = IEPerSecond;
                mie_generator.DelayTime = DelayTime;
            }
        }
    }

    [CoreBase]
    public abstract class m_stat_generator : core
    {

    }
}
