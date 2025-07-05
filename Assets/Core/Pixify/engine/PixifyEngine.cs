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
        Dictionary < Type, List <integral> > IndexedIntegrals = new Dictionary<Type, List<integral>> ();

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

        public void Register ( integral integral )
        {
            // find the base type
            Type current = integral.host.GetType ();

            while (!(current == typeof(object)))
            {
                if (current.GetCustomAttribute<IntegralBaseAttribute>() != null)
                {
                    RequestIntegralByIndex ( current ).Add ( integral );
                    return;
                }

                current = current.BaseType;
            }

            RequestIntegralByIndex ( integral.host.GetType () ).Add ( integral );
        }

        internal List<integral> RequestIntegralByIndex (Type t)
        {
            if ( IndexedIntegrals.TryGetValue (t, out List<integral> L) )
            return L;
            else
            {
                L = new List<integral>();
                IndexedIntegrals.Add (t, L);
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

    public abstract class PixifySytemBase
    {
        public abstract void Execute ();
    }

    public sealed class IntegralSystem <T> : PixifySytemBase where T : class, IIntegral
    {
        List <integral> integrals;
        public sealed override void Execute ()
        {
            for (int i = 0; i < integrals.Count; i++)
            {
                if (integrals [i].on)
                    integrals[i].Main();
            }
        }

        public IntegralSystem ()
        {
            integrals = PixifyEngine.o.RequestIntegralByIndex (typeof (T));
        }
    }

    public abstract class CustomIntegralSystem <T> : PixifySytemBase where T : class, IIntegral
    {
        List <integral> integrals;
        public override void Execute ()
        {
            for (int i = 0; i < integrals.Count; i++)
            {
                if (integrals [i].on)
                    Main (integrals[i].host as T);
            }
        }
        
        public CustomIntegralSystem ()
        {
            integrals = PixifyEngine.o.RequestIntegralByIndex (typeof (T));
        }

        protected abstract void Main (T o);
    }
}