using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{

    public class d_skill : pix
    {
        Dictionary <Type, skill_data> Skills = new Dictionary<Type, skill_data> ();

        public void AddSkill ( skill_data skill )
        {
            Skills.Add ( skill.GetType (), skill );
            b.IntegratePix ( skill );
        }

        public SKILL GetSkill <SKILL> () where SKILL : skill_data
        {
            return Skills [ typeof (SKILL) ] as SKILL;
        }

        public bool SkillValid <SKILL> () where SKILL : skill_data
        {
            if ( Skills.ContainsKey (typeof (SKILL)) )
                return Skills [ typeof (SKILL) ].SkillCondition ();
            return false;
        }
    }

    public abstract class skill_data : pix
    {
        public abstract bool SkillCondition ();
    }
}