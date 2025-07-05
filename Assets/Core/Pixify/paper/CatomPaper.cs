using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [Serializable]
    public struct AtomPaper <T> where T:atom
    {
        [SerializeField]
        public string StrNodeType;

        [SerializeField]
        public string StrNodeData;

        public T Write ( ) 
        {
            if ( !string.IsNullOrEmpty (StrNodeType) && Type.GetType (StrNodeType) != null )
            {
                return atom.Write <T> ( StrNodeType, StrNodeData );
            }
            else
            {
                Debug.LogError ("Node type not found: " + StrNodeType);
                return null;
            }
        }
    }

    [Serializable]
    public struct CatomPaper <T> where T:catom
    {
        [SerializeField]
        public string StrNodeType;

        [SerializeField]
        public string StrNodeData;

        public T Write ( ICatomFactory factory ) 
        {
            if ( !string.IsNullOrEmpty (StrNodeType) && Type.GetType (StrNodeType) != null )
            {
                return catom.New <T> ( StrNodeType, StrNodeData, factory );
            }
            else
            {
                Debug.LogError ("Node type not found: " + StrNodeType);
                return null;
            }
        }
    }
}