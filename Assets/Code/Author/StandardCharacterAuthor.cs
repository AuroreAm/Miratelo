using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class StandardCharacterAuthor : AuthorModule
    {
        public script main;

        public override ModuleWriter[] GetModules()
        {
            return new ModuleWriter[] {};
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