using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class Character : MonoBehaviour
    {
        // library of unique module indexed by type
        // polymorphism not supported
        protected Dictionary < Type, module > modules;
        List < node > nodes;
        uint ptr;

        void Awake ()
        {
            modules = new Dictionary<Type, module> ();
            nodes = new List<node> ();
        }

        /// <summary>
        /// return a module of the type requested
        /// if the module is not found, it will be created
        /// does not work with ctor arguments
        /// </summary>
        public module RequireModule (Type moduleType)
        {
            if (!moduleType.IsSubclassOf(typeof (module)))
                throw new InvalidOperationException("cannot depend on a non module type");

            if (modules.TryGetValue(moduleType, out module m))
                return m;
            else
            {
                m = Activator.CreateInstance(moduleType) as module;
                modules.Add(moduleType, m);

                m.character = this;
                RegisterNode (m);
                m.Create ();

                return m;
            }
        }

        /// <summary>
        /// will add or replace current module with this type
        /// use this if need a module with constructor arguments
        /// </summary>
        /// <param name="m"></param>
        public void PushModule ( module m )
        {
            modules.AddOrChange ( m.GetType(), m );
            m.character = this;
            RegisterNode (m);
            m.Create ();
        }

        void RegisterNode ( node m )
        {
            ptr ++;
            m.nodeId = ptr;
            nodes.Add (m);

            Type current = m.GetType ();
            while ( current != typeof ( node ) )
            {
                var fis = current.GetFields( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var fi in fis)
                {
                if (fi.GetCustomAttribute<DependAttribute>() != null)
                    fi.SetValue ( m, RequireModule(fi.FieldType) );
                }
                current = current.BaseType;
            }
        }

        /// <summary>
        /// connect and initialize this node with the character if it is not connected
        /// </summary>
        public node ConnectNode ( node n )
        {
            if (!nodes.Contains (n))
            {
                RegisterNode (n);
                n.Create ();
            }
            return n;
        }

        public T ConnectNode <T> (T a) where T:node
        {
            if (!nodes.Contains (a))
            {
                RegisterNode (a);
                a.Create ();
            }
            return a;
        }
    }
}