using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("sword skill")]
    public abstract class sword_skill : skill_data
    {
        [Depend]
        m_equip me;

        public override bool SkillCondition()
        {
            return me.weaponUser is m_sword_user ;
        }
    }

    public class SS2_consecutive : sword_skill
    {
        [Depend]
        m_equip me;

        public task slash_0, slash_1, slash_2;

        public override void Create()
        {
            var ac_slash_0 = New <ac_slash> (me.character);
            ac_slash_0.ComboId = 0;
            slash_0 = motor_main_task.New ( me.character, ac_slash_0 );

            var ac_slash_1 = New <ac_slash> (me.character);
            ac_slash_1.ComboId = 1;
            slash_1 = motor_main_task.New ( me.character, ac_slash_1 );

            var ac_slash_2 = New <ac_slash> (me.character);
            ac_slash_2.ComboId = 2;
            slash_2 = motor_main_task.New ( me.character, ac_slash_2 );
        }
    }

    public class SS3_hooker_up : sword_skill
    {
    }

    public class SS4_knocker : sword_skill
    {
    }
}