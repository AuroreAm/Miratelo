using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    // create a character from a model
    public class CharacterWriter : MonoBehaviour
    {
        [SerializeField]
        private CharacterModel Model;

        [SerializeReference]
        public List <ModuleWriter> OverrideParameters;

        // default module the character will use, added after all modules are added
        public m_character_controller_writer OverrideController;

        void Awake()
        {
            var c = gameObject.AddComponent<Character>();
            
            List <Type> OverridedTypes = new List<Type>();

            // write override first
            foreach (var p in OverrideParameters)
            {
                p.WriteModule(c);
                OverridedTypes.Add(p.GetType());
            }
            // write from model
            if (Model)
            foreach (var p in Model.Parameters)
            {
                if (!OverridedTypes.Contains(p.GetType()))
                    p.WriteModule(c);
            }
            // add the character controller
            if (OverrideController!=null)
                OverrideController.WriteModule(c);

            Destroy (this);
        }

        [Serializable]
        public class m_character_controller_writer : ModuleWriter
        {
            public ScriptModel Script;

            public override void WriteModule (Character character)
            {
                ActionModel root = null;
                if (Script) 
                {
                    root = Script.Root;

                    if (root !=null || root.Valid)
                    {
                        m_character_controller c = character.RequireModule<m_character_controller> ();
                        // set the character controller root
                        c.root = root.CreateNode(character);
                        // self aquire character controller
                        c.Aquire(new Void());
                    }
                    else
                    Debug.LogWarning("Model has no root");
                }
            }
        }
    }
}