using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using System;

namespace Triheroes.Code
{
    // for players character with humanoid characteristics
    // don't need character controller here, automatically added by game master
    public class StandardPlayerAuthor : Writer
    {
        [Header("Skin (appearance)")]
        public skin_writer skin;
        [Header("Actor definition")]
        public actor_writer actor;
        [Header("Stats")]
        public stat_writer stat;
        [Header("Skills")]
        public skill_writer skill;

        public override void AfterSpawn(Vector3 position, Quaternion rotation, block b)
        {
            skin.AfterWrite (b);
            actor.AfterWrite (b);
            stat.AfterWrite (b);
            skill.AfterWrite (b);

            b.GetPix <s_skin> ().rotY = rotation.eulerAngles;
        }

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

            return a.ToArray ();
        }
    }
}