using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;

namespace Lyra
{
    [DefaultExecutionOrder(-1500)]
    public sealed class phoenix : MonoBehaviour, creator
    {
        public static system star {private set; get;}
        public static phoenix o;
        public static core core;

        void Awake ()
        {
            load_star_execution_order ();
            load_phoenix ();
        }

        void LateUpdate ()
        {
            core.pulse ();
        }

        void load_star_execution_order ()
        {
            Type [] order =
            AppDomain.CurrentDomain.GetAssemblies ().SelectMany ( a => a.GetTypes () )
            .Where ( t => t.GetCustomAttribute <starAttribute> () != null )
            .OrderBy ( t => t.GetCustomAttribute<starAttribute> ()!.order )
            .ToArray ();

            core = new core ( order );
        }

        void load_phoenix ()
        {
            o = this;

            var A = AppDomain.CurrentDomain.GetAssemblies();
            List <Type> superstar = new List<Type> ();

            foreach (var y in A)
            foreach (Type x in y.GetTypes())
            {
                if ( x.GetCustomAttribute <superstarAttribute> () != null )
                superstar.Add (x);
            }
            
            star = new system.creator ( superstar.ToArray (), this ).create_system ();
        }

        public void _create() {}
        public void _created (system s) {}
    }

    public sealed class superstarAttribute : Attribute
    {}
}