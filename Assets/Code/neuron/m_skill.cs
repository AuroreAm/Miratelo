using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Serializable]
    public class skill_writer : ModuleWriter
    {
        public CatomPaper <skill_data> [] skills;

        public override void WriteModule(Character character)
        {
            m_skill ms = character.RequireModule <m_skill> ();
            for (int i = 0; i < skills.Length; i++)
            {
                ms.AddSkill ( skills [i].Write (character) );
            }
        }
    }

    public class m_skill : module
    {
        Dictionary <Type, skill_data> Skills = new Dictionary<Type, skill_data> ();

        public void AddSkill ( skill_data skill )
        {
            Skills.Add ( skill.GetType (), skill );
        }

        public bool SkillValid <SKILL> () where SKILL : skill_data
        {
            if ( Skills.ContainsKey (typeof (SKILL)) )
                return Skills [ typeof (SKILL) ].SkillCondition ();
            return false;
        }
    }

    public abstract class skill_data : catom
    {
        public abstract bool SkillCondition ();
    }
}