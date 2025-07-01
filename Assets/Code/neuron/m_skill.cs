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
        public NodePaper <skill_data> [] skills;

        public override void WriteModule(Character character)
        {
            m_skill ms = character.RequireModule <m_skill> ();
            for (int i = 0; i < skills.Length; i++)
            {
                ms.AddSkill ( skills [i].WriteNode () );
            }
        }
    }

    public class m_skill : module
    {
        Dictionary <Type, skill_data> Skills = new Dictionary<Type, skill_data> ();

        /// <summary>
        /// add skill data, and connect it to the character
        /// </summary>
        /// <param name="skill"></param>
        public void AddSkill ( skill_data skill )
        {
            Skills.Add ( skill.GetType (), character.ConnectNode (skill) );
        }

        public bool SkillValid <SKILL> () where SKILL : skill_data
        {
            if ( Skills.ContainsKey (typeof (SKILL)) )
                return Skills [ typeof (SKILL) ].SkillCondition ();
            return false;
        }
    }

    public abstract class skill_data : node
    {
        public abstract bool SkillCondition ();
    }
}