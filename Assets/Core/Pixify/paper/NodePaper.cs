using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    /// <summary>
    /// Contains a blueprint of a node
    /// </summary>
    [Serializable]
    public sealed class NodePaper : ISerializationCallbackReceiver
    {
        // blueprint saved data
        [SerializeField]
        [HideInInspector]
        string StrNodeType;
        [SerializeField]
        [HideInInspector]
        string StrNodeJson;

        #if UNITY_EDITOR
        // used by node editors to edit the blueprint
        public node blueprint;
        #endif

        //runtime node instantiation
        public node CreateNode()
        {
            if ( Type.GetType (StrNodeType) != null )
            {
                node n = (node) Activator.CreateInstance (Type.GetType (StrNodeType));
                JsonUtility.FromJsonOverwrite (StrNodeJson, n);
                return n;
            }
            else
            {
                Debug.LogError ("Node type not found: " + StrNodeType);
                return null;
            }
        }

        #if UNITY_EDITOR
        public NodePaper Set (Type t)
        {
            blueprint = (node) Activator.CreateInstance (t);
            return this;
        }
        #endif

        public void OnAfterDeserialize()
        {
            // after deserialization, load the blueprint from the strings // only for editor
            #if UNITY_EDITOR
            if ( !string.Equals (StrNodeType, string.Empty) )
            {
                // if the type is invalid, try to get if the type was renamed
                if ( Type.GetType (StrNodeType) == null )
                {
                    if ( ClassMigration.TypeWithFormer (StrNodeType, out Type t) )
                        StrNodeType = t.AssemblyQualifiedName;
                    else
                    {
                        Debug.LogError ("Node type not found: " + StrNodeType);
                        StrNodeType = string.Empty;
                        StrNodeJson = string.Empty;
                        return;
                    }
                }

                blueprint = (node) Activator.CreateInstance (Type.GetType (StrNodeType));
                JsonUtility.FromJsonOverwrite (StrNodeJson, blueprint);
            }
            #endif
        }

        public void OnBeforeSerialize()
        {
            // before serialization, save the blueprint into the strings
            if ( blueprint != null )
            {
                StrNodeType = blueprint.GetType().AssemblyQualifiedName;
                StrNodeJson = JsonUtility.ToJson(blueprint);
            }
        }
    }
}