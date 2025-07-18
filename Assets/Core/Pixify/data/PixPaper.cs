using System.Reflection;
using System;
using UnityEngine;

namespace Pixify
{
    [Serializable]
    public struct PixPaper <T> where T:pix
    {
        [SerializeField]
        public string StrNodeType;

        [SerializeField]
        public string StrNodeData;

        public T Write ( ) 
        {
            if ( !string.IsNullOrEmpty (StrNodeType) && Type.GetType (StrNodeType) != null )
            {
                T p = Activator.CreateInstance ( Type.GetType (StrNodeType) ) as T;
                JsonUtility.FromJsonOverwrite ( StrNodeData, p );
                return p;
            }
            else
            {
                Debug.LogError ("Node type not found: " + StrNodeType);
                return null;
            }
        }
    }
}
