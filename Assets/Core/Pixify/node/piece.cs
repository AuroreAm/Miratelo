using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public abstract class piece : atom, IIntegral
    {
        public Unit unit;

        public virtual void Create ()
        {}

        public integral integral {private set; get;}
        public piece ()
        {
            integral = new integral (this, OnStart, OnFree);
        }

        public abstract void Main();

        protected virtual void OnStart ()
        {}

        protected virtual void OnFree ()
        {}

        public void Aquire (atom host) => integral.Aquire (host);
        public void Free (atom host) => integral.Free (host);
    }

    
    [Serializable]
    public struct PieceSkin
    {
        public Vector3 RotY;
        public Mesh Mesh;
        public Material Material;
    }
}