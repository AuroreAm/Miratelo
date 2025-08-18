using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{

    public class d_skill : pix
    {
        Dictionary <Type, skill_box> Skills = new Dictionary<Type, skill_box> ();

        public void AddSkill ( skill_box skill )
        {
            Skills.Add ( skill.GetType (), skill );
            b.IntegratePix ( skill );
        }

        public SKILL GetSkill <SKILL> () where SKILL : skill_box
        {
            return Skills [ typeof (SKILL) ] as SKILL;
        }

        public bool SkillValid <SKILL> () where SKILL : skill_box
        {
            if ( Skills.ContainsKey (typeof (SKILL)) )
                return Skills [ typeof (SKILL) ].SkillValid;
            return false;
        }
    }

    public abstract class skill_box : pix
    {
        public virtual bool SkillValid => true;
    }

}