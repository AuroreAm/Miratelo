using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class Character : MonoBehaviour, ICatomFactory
    {
        // library of unique module indexed by type
        // polymorphism not supported
        protected Dictionary < Type, module > modules;

        // library of unique action for the character
        public Dictionary < Type, action > actionLibrary { private set; get; } = new Dictionary<Type, action> ();

        List < atom > atoms;
        uint ptr;

        public Character character => this;
        public void AfterCreateInstance ( catom catom )
        {
            ptr ++;
            catom.atomId = ptr;
            atoms.Add (catom);

            if (_HotRequireModule)
            {
                modules.Add(catom.GetType (), (module) catom);
                _HotRequireModule = false;
            }

            if (_HotRequireAction)
            {
                actionLibrary.Add(catom.GetType (), (action) catom);
                _HotRequireAction = false;
            }

            Type current = catom.GetType ();
            while ( current != typeof ( catom ) )
            {
                var fis = current.GetFields( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var fi in fis)
                {
                if (fi.GetCustomAttribute<DependAttribute>() != null)
                    fi.SetValue ( catom, RequireAtom(fi.FieldType) );
                }
                current = current.BaseType;
            }

            catom.Create ();
        }

        public T GetAction <T> () where T : action
        {
            if (actionLibrary.TryGetValue(typeof(T), out action n))
                return n as T;
            else
            throw new InvalidOperationException("cannot find unique action of type " + typeof(T));
        }

        void Awake ()
        {
            modules = new Dictionary<Type, module> ();
            atoms = new List<atom> ();
        }

        atom RequireAtom (Type AtomType)
        {
            if (AtomType.IsSubclassOf(typeof (action)))
                return RequireAction (AtomType);
            else if (AtomType.IsSubclassOf(typeof (module)))
                return RequireModule (AtomType);
            else
                throw new InvalidOperationException("cannot depend on a non action or module type");
        }

        bool _HotRequireAction;
        action RequireAction (Type actiontype)
        {
            if (!actiontype.IsSubclassOf(typeof (action)))
                throw new InvalidOperationException("cannot depend on a non action type");

            if (actionLibrary.TryGetValue(actiontype, out action a))
                return a;
            else
            {
                _HotRequireAction = true;
                a = catom.New <action> (actiontype, this);
                return a;
            }
        }

        bool _HotRequireModule;
        module RequireModule (Type moduleType)
        {
            if (!moduleType.IsSubclassOf(typeof (module)))
                throw new InvalidOperationException("cannot depend on a non module type");

            if (modules.TryGetValue(moduleType, out module m))
                return m;
            else
            {
                _HotRequireModule = true;
                m = catom.New <module> (moduleType, this);

                return m;
            }
        }

        /// <summary>
        /// return a module of the type requested
        /// if the module is not found, it will be created
        /// does not work with constructor arguments
        /// </summary>
        public T RequireModule <T> () where T : module, new ()
        {
            if (modules.TryGetValue(typeof(T), out module m))
                return m as T;
            else
            {
                _HotRequireModule = true;
                var n = catom.New <T> (this);
                return n;
            }
        }

        public bool HaveModule <T> ()
        {
            return modules.ContainsKey (typeof(T));
        }

        public T GetModule <T> () where T : module
        {
             if (modules.TryGetValue(typeof(T), out module m))
                return m as T;
            else
                return null;
        }

        #if UNITY_EDITOR
        public Action OnDrawGizmos;
        public void OnDrawGizmosSelected(  )
        {
            OnDrawGizmos?.Invoke ();
        }
        #endif
    }
}