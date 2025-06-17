using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    // for players character with humanoid characteristics
    public class StandardPlayerAuthor : AuthorModule
    {
        [Header("Skin (appearance)")]
        public skin_writer skin;

        [Header("Actor definition")]
        public actor_writer actor;

        [Header("Stats")]
        public m_stat_writer stat;

        override public ModuleWriter[] GetModules ()
        {
            return new ModuleWriter[] { skin, actor };
        }

        override public void OnSpawn ( Vector3 position, Quaternion rotation, Character c )
        {
            c.GetModule <m_skin> ().rotY = rotation.eulerAngles;
        }
    }
}