using System;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class SkillWriter : Writer
    {
        public PixPaper <skill_box> [] skills;

        public override void RequiredPix(in List<Type> a)
        {
            a.A <d_skill> ();
        }

        public override void AfterWrite(block b)
        {
            for (int i = 0; i < skills.Length; i++)
            b.GetPix <d_skill> ().AddSkill ( skills [i].Write () );
        }

        public override void OnWriteBlock()
        {
        }
    }
}
