using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class StandardCharacterAuthor : Writer
    {
        public PixPaper<cortex> MainCortex;
        public RolePlay AI;

        [Header("Skin (appearance)")]
        public skin_writer skin;
        [Header("Actor definition")]
        public actor_writer actor;
        [Header("Stats")]
        public stat_writer stat;
        [Header("Skill")]
        public skill_writer skill;
        
        public override void OnWriteBlock()
        {
            skin.OnWriteBlock ();
            actor.OnWriteBlock ();
            stat.OnWriteBlock ();
            skill.OnWriteBlock ();
        }

        public override Type[] RequiredPix()
        {
            var a = new List <Type> ();
            
            skin.RequiredPix ( in a );
            actor.RequiredPix ( in a );
            stat.RequiredPix ( in a );
            skill.RequiredPix ( in a ); 

            a.A <s_mind> ();

            return a.ToArray ();
        }

        public override void AfterSpawn(Vector3 position, Quaternion rotation, block b)
        {
            skin.AfterWrite (b);
            actor.AfterWrite (b);
            stat.AfterWrite (b);
            skill.AfterWrite (b);

            b.GetPix <s_skin> ().rotY = rotation.eulerAngles;

            b.GetPix <s_mind> ().SetCortex ( MainCortex.Write () );
            var AIBehaviors = AI.GetThoughtConcepts (b);
            b.GetPix <s_mind> ().AddConcepts ( AIBehaviors );
            b.GetPix <s_mind> ().master.StartRootThought ( AIBehaviors [0].Item2 );
        }

    }
}