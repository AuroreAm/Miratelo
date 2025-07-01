using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class StandardCharacterAuthor : Scripter
    {
        public NodePaper<cortex> MainCortex;

        [Header("Skin (appearance)")]
        public skin_writer skin;
        [Header("Actor definition")]
        public actor_writer actor;
        [Header("Stats")]
        public stat_writer stat;
        [Header("Skill")]
        public skill_writer skill;

        override public ModuleWriter[] GetModules ()
        {
            return new ModuleWriter[] { skin, actor, stat, skill };
        }

        override public void OnSpawn ( Vector3 position, Quaternion rotation, Character c )
        {
            c.GetModule <m_skin> ().rotY = rotation.eulerAngles;
            c.RequireModule <m_cortex> ().SetCortex ( MainCortex.WriteNode () );
        }
    }
}