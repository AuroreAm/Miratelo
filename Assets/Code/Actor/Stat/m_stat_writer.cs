using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("Actor")]
    public class m_stat_writer : ModuleWriter
    {
        public float MaxHP;

        public override void WriteModule(Character character)
        {
            if (MaxHP > 0)
            {
                var mshp = character.RequireModule<m_stat_HP>();
                mshp.Set(MaxHP);
            }
        }
    }
}
