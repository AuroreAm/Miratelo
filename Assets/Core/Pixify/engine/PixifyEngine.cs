using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Pixify
{
    // derive the sceneMaster from this to create the gamesystem
    public abstract class PixifyEngine : MonoBehaviour
    {
        public static PixifyEngine o;

        List<PixifySytemBase> Systems;
        Dictionary < Type, List <core> > IndexedCores = new Dictionary<Type, List<core>> ();

        public abstract void BeforeCreateSystems ();
        public abstract void AfterCreateSystems ();

        void Awake ()
        {
            o = this;
            BeforeCreateSystems ();
            CreateSystems ( out Systems );
            AfterCreateSystems ();
        }

        protected abstract void CreateSystems ( out List<PixifySytemBase> systems );

        public void Register ( core core )
        {
            RequestListCoresOfType ( core.GetType ().GetCustomAttribute<RegisterAsBaseAttribute>() == null? core.GetType(): core.GetType().BaseType ).Add ( core );
        }

        internal List<core> RequestListCoresOfType (Type t)
        {
            if ( IndexedCores.TryGetValue (t, out List<core> L) )
            return L;
            else
            {
                L = new List<core>();
                IndexedCores.Add (t, L);
                return L;
            }
        }

        void LateUpdate ()
        {
            int count = Systems.Count;
            for (int i = 0; i < count; i++)
            {
                Systems [i].Execute ();
            }
        }
    }

    /// <summary>
    /// register the type as its base type instead of the derived type
    /// </summary>
    public class RegisterAsBaseAttribute : Attribute
    {}

    public abstract class PixifySytemBase
    {
        public abstract void Execute ();
    }

    public sealed class CoreSystem <T> : PixifySytemBase where T : core
    {
        List <core> cores;
        public sealed override void Execute ()
        {
            for (int i = 0; i < cores.Count; i++)
            {
                if (cores [i].on)
                    cores[i].Main();
            }
        }

        public CoreSystem ()
        {
            cores = PixifyEngine.o.RequestListCoresOfType (typeof (T));
        }
    }

    public abstract class CustomCoreSystem <T> : PixifySytemBase where T : core
    {
        List <core> cores;
        public override void Execute ()
        {
            for (int i = 0; i < cores.Count; i++)
            {
                if (cores [i].on)
                    Main (cores[i] as T);
            }
        }
        
        public CustomCoreSystem ()
        {
            cores = PixifyEngine.o.RequestListCoresOfType (typeof (T));
        }

        protected abstract void Main (T o);
    }
}