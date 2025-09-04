using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{

    [Path ("")]
    public abstract class dat
    {
        static uint s_counter = 0;
        public uint InstanceId { get; private set; }
        private bool Ready;
        public structure Structure {get; private set;}

        public dat()
        {
            InstanceId = ++s_counter;

            if (s_domain != null)
            s_domain.Add ( this );
        }

        static structure s_domain;

        protected virtual void OnStructured ()
        {}

        private void iStructure ( structure structure )
        {
            if ( GetType ().GetCustomAttribute <NeedPackage> () != null && !Ready )
                Debug.LogError ( $"{GetType ().Name} was structured without package" );

            Structure = structure;
            BeginStructureDomain ();
            OnStructured ();
            EndStructureDomain();
        }

        protected void BeginStructureDomain()
        {
            if (s_domain == null)
                s_domain = Structure;
        }

        protected void EndStructureDomain()
        {
            if (s_domain == Structure)
                s_domain = null;
        }

        public class structure
        {
            Dictionary<Type, dat> _main = new Dictionary<Type, dat>();
            List <dat> _brick = new List<dat> ();

            private structure ( Dictionary < Type, dat > founders )
            {
                _brick.AddRange ( founders.Values );

                foreach ( var kvp in founders )
                _main.Add (kvp.Key, kvp.Value);

                var founderBrick = _brick.ToArray ();
                foreach ( var d in founderBrick)
                InjectLink ( d );

                foreach ( var d in founderBrick )
                d.iStructure ( this );

                foreach ( var d in founderBrick )
                _newMember ( d );
            }

            public void Add ( dat item )
            {
                if ( _brick.Contains ( item ) )
                return;

                _brick.Add ( item );
                if ( ! _main.ContainsKey ( item.GetType () ) )
                _main.Add ( item.GetType (), item );

                InjectLink ( item );

                item.iStructure ( this );
                _newMember ( item );
            }

            void InjectLink (dat item)
            {
                Type current = item.GetType();
                while (current != typeof(dat))
                {
                    var fis = current.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    foreach (var fi in fis)
                    {
                        if (fi.GetCustomAttribute<LinkAttribute>() != null)
                            fi.SetValue(item, RequireDat(fi.FieldType));
                    }
                    current = current.BaseType;
                }
            }

            dat RequireDat ( Type t )
            {
                if ( _main.ContainsKey(t) )
                    return _main[t];
                else
                {
                    var c = (dat)Activator.CreateInstance(t);
                    Add(c);
                    return c;
                }
            }

            public T Get<T>() where T : dat
            {
                if (_main.TryGetValue(typeof(T), out dat m))
                    return m as T;
                else
                    return null;
            }

            // local callback
            event Action<dat> _newMember = delegate { };
            public event Action<dat> OnNewMember
            {
                add { _newMember += value; }
                remove { _newMember -= value; }
            }

            public class Creator
            {
                internal static Creator _o { private set; get; }
                IStructureAuthor _author;
                Dictionary <Type,dat> _founder = new Dictionary<Type, dat> ();

                public Creator ( IStructureAuthor author )
                {
                    _author = author;
                }

                public Creator ( Type [] types, IStructureAuthor author )
                {
                    _author = author;

                    foreach ( var x in types )
                    RequireFounder ( x );
                }

                static internal T Querry <T> () where T : dat => _o.RequireFounder ( typeof (T) ) as T;

                dat RequireFounder ( Type type )
                {
                    if ( _founder.ContainsKey ( type ) )
                        return _founder [type];
                    else
                    {
                        var c = Activator.CreateInstance (type) as dat;
                        _founder.Add ( type, c );
                        SetDependency ( type );
                        return c;
                    }
                }

                void SetDependency ( Type type )
                {
                    Type current = type;

                    if (!type.IsSubclassOf (typeof (dat)))
                    throw new InvalidOperationException ( "only dat type can be in Structure" );
            
                    while ( current != typeof ( dat ) )
                    {
                        var fis = current.GetFields( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        foreach (var fi in fis)
                        {
                        if (fi.GetCustomAttribute<LinkAttribute>() != null)
                            RequireFounder (fi.FieldType);
                        }
                        current = current.BaseType;
                    }
                }

                public structure CreateStructure ()
                {
                    _o = this;
                    _author.OnStructure ();
                    _o = null;

                    return new structure ( _founder );
                }

            }
        }
        
        public static T Q <T> () where T : dat, new()
        {
            if (structure.Creator._o == null)
                    throw new InvalidOperationException("Querry can only be called in OnStructure ()");

            return structure.Creator.Querry <T> ();
        }
        public abstract class Package<T> where T : dat, new()
        {
            public Package()
            {
                if (structure.Creator._o == null)
                    throw new InvalidOperationException("package can only be instantied in OnStructure ()");

                o = structure.Creator.Querry <T> ();
                o.Ready = true;
            }

            /// <summary>
            /// the target dat
            /// </summary>
            protected T o { private set; get; }
        }
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class PathAttribute : Attribute
    {
        public string Name { get; }

        public PathAttribute(string Category)
        { Name = Category; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class LinkAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Field)]
    public class ExportAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Class)]
    public class NeedPackage : Attribute
    { }
}

// TODO: reflection cache for dependency