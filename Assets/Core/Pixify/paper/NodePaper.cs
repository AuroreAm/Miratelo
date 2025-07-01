using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [Serializable]
    public struct NodePaper <T> where T:node
    {
        [SerializeField]
        public string StrNodeType;

        [SerializeField]
        public string StrNodeData;

        public T WriteNode () 
        {
            if ( !string.IsNullOrEmpty (StrNodeType) && Type.GetType (StrNodeType) != null )
            {
                node n = (node) Activator.CreateInstance (Type.GetType (StrNodeType));
                JsonUtility.FromJsonOverwrite (StrNodeData, n);
                return n as T;
            }
            else
            {
                Debug.LogError ("Node type not found: " + StrNodeType);
                return null;
            }
        }
    }
}