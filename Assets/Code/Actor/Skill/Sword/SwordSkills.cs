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

        [Depend]
        m_cortex mc;

        [Depend]
        ac_SS2 SS2;

        [Depend]
        ac_SS2.ac_SS2_next_combo SS2_next;	

        public override void Create()
        {
            Notion[] SlashNotion = new Notion [] 
                { 
                    new Notion( SlashCondition, commands.draw_sword )
                };

            bool SlashCondition ()
            {
                return me.weaponUser is m_sword_user;
            }

            plan_notion n_SS2 = new plan_notion ( SlashNotion, SS2, commands.SS2 );
            plan_notion n_SS2_next = new plan_notion ( SlashNotion, SS2_next, commands.SS2_next );
            mc.AddNotion ( n_SS2 );
            mc.AddNotion ( n_SS2_next );
        }
    }

    public class SS3_hooker_up : sword_skill
    {
    }

    public class SS4_knocker : sword_skill
    {
    }
}