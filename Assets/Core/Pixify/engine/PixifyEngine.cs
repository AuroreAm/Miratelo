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

        List<CoreSystemBase> Systems;
        Dictionary < Type, List <core> > IndexedCores = new Dictionary<Type, List<core>> ();

        public abstract void BeforeCreateSystems ();

        void Awake ()
        {
            o = this;
            BeforeCreateSystems ();
            CreateSystems ( out Systems);

            for (int i = 0; i < Systems.Count; i++)
            Systems[i].cores = RequestListModulesOfType (Systems[i].SystemType ());
        }

        protected abstract void CreateSystems ( out List<CoreSystemBase> systems );

        public void Register ( core core )
        {
            RequestListModulesOfType ( core.GetType ().GetCustomAttribute<RegisterAsBaseAttribute>() == null? core.GetType(): core.GetType().BaseType ).Add ( core );
        }

        List<core> RequestListModulesOfType (Type t)
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

    public abstract class CoreSystemBase
    {
        public List <core> cores;
        public abstract void Execute ();
        public abstract Type SystemType ();
    }

    public sealed class CoreSystem <T> : CoreSystemBase where T : core
    {
        public sealed override void Execute ()
        {
            for (int i = 0; i < cores.Count; i++)
            {
                if (cores [i].on)
                    cores[i].Main();
            }
        }

        public sealed override Type SystemType () => typeof (T);
    }

    public abstract class CustomCoreSystem <T> : CoreSystemBase where T : core
    {
        public override void Execute ()
        {
            for (int i = 0; i < cores.Count; i++)
            {
                if (cores [i].on)
                    Main (cores[i] as T);
            }
        }
        
        public override Type SystemType () => typeof (T);

        protected abstract void Main (T o);
    }
}