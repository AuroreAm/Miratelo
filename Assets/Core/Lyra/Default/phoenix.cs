using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;

namespace Lyra
{
    [DefaultExecutionOrder(-1500)]
    public sealed class phoenix : MonoBehaviour, IAuthor
    {
        public static shard.constelation galaxy {private set; get;}
        public static core core;

        void Awake ()
        {
            LoadMelody ();
            LoadLead ();
        }

        void LateUpdate ()
        {
            core.pulse ();
        }

        void LoadMelody ()
        {
            Type [] melody =
            AppDomain.CurrentDomain.GetAssemblies ().SelectMany ( a => a.GetTypes () )
            .Where ( t => t.GetCustomAttribute <noteAttribute> () != null )
            .OrderBy ( t => t.GetCustomAttribute<noteAttribute> ()!.pitch )
            .ToArray ();

            core = new core ( melody );
        }

        void LoadLead ()
        {
            var A = AppDomain.CurrentDomain.GetAssemblies();
            List <Type> lead = new List<Type> ();

            foreach (var y in A)
            foreach (Type x in y.GetTypes())
            {
                if ( x.GetCustomAttribute <leadAttribute> () != null )
                lead.Add (x);
            }
            
            galaxy = new shard.constelation.write ( lead.ToArray (), this ).constelation ();
        }

        public void writings() {}
    }

    public sealed class leadAttribute : Attribute
    {}
}