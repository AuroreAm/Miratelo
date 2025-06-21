using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class StandardCharacterAuthor : Scripter
    {
        [Header("Skin (appearance)")]
        public skin_writer skin;
        [Header("Actor definition")]
        public actor_writer actor;
        [Header("Stats")]
        public m_stat_writer stat;

        [Header("Behavior")]
        public script main;

        override public ModuleWriter[] GetModules ()
        {
            return new ModuleWriter[] { skin, actor, stat };
        }

        public override void OnSpawn(Vector3 position, Quaternion rotation, Character c)
        {
            if (main)
            {
                m_character_controller mcc = c.RequireModule<m_character_controller> ();
                treeBuilder.TreeStart ( c );
                mcc.StartRoot ( main.CreateTree () );
            }
            else
            Debug.LogWarning (" A spawner has no main script");
        }
    }
}