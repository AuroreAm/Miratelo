using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Serializable]
    public class skill_writer : PixWriter
    {
        public PixPaper <skill_data> [] skills;

        public override void RequiredPix(in List<Type> a)
        {
            a.A <d_skill> ();
        }

        public override void AfterWrite(block b)
        {
            for (int i = 0; i < skills.Length; i++)
            b.GetPix <d_skill> ().AddSkill ( skills [i].Write () );
        }
    }

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