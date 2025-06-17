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
            var cc = GetComponent <script> ();
            if (cc)
            {
                m_character_controller mcc = c.RequireModule<m_character_controller> ();
                treeBuilder.TreeStart ( c );
                mcc.StartRoot (cc.CreateTree ());
            }

            Destroy (this);
        }
    }
}