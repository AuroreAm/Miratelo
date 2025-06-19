using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public abstract class piece : node
    {
        public Unit unit;

        public bool on { get; private set; }

        public piece ()
        {
            #if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            #endif
            PixifyEngine.o.Register (this);
        }

        public abstract void Main();

        public void iStart()
        {
            on = true;
            OnStart();
        }
        
        public void iFree()
        {
            on = false;
            OnFree();
        }

        protected virtual void OnStart ()
        {}

        protected virtual void OnFree ()
        {}
    }

    
    [Serializable]
    public struct PieceSkin
    {
        public Vector3 RotY;
        public Mesh Mesh;
        public Material Material;
    }
}