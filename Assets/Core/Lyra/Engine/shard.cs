using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

 // . ݁₊ ⊹ . ݁˖ . ݁  //
namespace Lyra
{
    [verse ("")]
    public abstract class shard
    {
        static uint hán  = 0;
        /// <summary> InstanceId </summary>
        public uint sei  { get; private set; }

        // True if the shard is state ready, this is use when the shard type has the [NeedInk] attribute 
        private bool ready;

        /// <summary> The parent constelation of this shard </summary>
        public constelation sky {get; private set;}

        public shard()
        {
            sei  = ++hán;

            if (within != null)
            within.add ( this );
        }

        // current constelation domain, new instance of shard is automatically added to the current domain
        static constelation within;

        protected virtual void harmony ()
        {}

        private void iOrbit ( constelation structure )
        {
            if ( GetType ().GetCustomAttribute <inkedAttribute> () != null && !ready )
                Debug.LogError ( $"{GetType ().Name} was structured without package" );

            sky = structure;

            DiveInDomain ();
            harmony ();
            ExitDomain();
        }

        protected void DiveInDomain()
        {
            if (within == null)
                within = sky;
        }

        protected void ExitDomain()
        {
            if (within == sky)
                within = null;
        }

        public class constelation
        {
            // main shards, indexed by type
            Dictionary<Type, shard> stars = new Dictionary<Type, shard>();
            // every over shards
            List <shard> shards = new List<shard> ();

            private constelation ( Dictionary < Type, shard > founders )
            {
                shards.AddRange ( founders.Values );

                foreach ( var kvp in founders )
                stars.Add (kvp.Key, kvp.Value);

                var founderBrick = shards.ToArray ();
                foreach ( var d in founderBrick)
                LinkEachOther ( d );

                foreach ( var d in founderBrick )
                d.iOrbit ( this );

                foreach ( var d in founderBrick )
                _newShard ( d );
            }

            public void add ( shard item )
            {
                if ( shards.Contains ( item ) )
                return;

                shards.Add ( item );
                if ( ! stars.ContainsKey ( item.GetType () ) )
                stars.Add ( item.GetType (), item );

                LinkEachOther ( item );

                item.iOrbit ( this );
                _newShard ( item );
            }

            void LinkEachOther (shard item)
            {
                Type current = item.GetType();
                while (current != typeof(shard))
                {
                    var fis = current.GetFields ( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public );
                    foreach (var fi in fis)
                    {
                        if (fi.GetCustomAttribute<harmonyAttribute>() != null)
                            fi.SetValue(item, LookForShard (fi.FieldType));
                    }
                    current = current.BaseType;
                }
            }

            shard LookForShard ( Type t )
            {
                if ( stars.ContainsKey(t) )
                    return stars[t];
                else
                {
                    var c = (shard)Activator.CreateInstance(t);
                    add(c);
                    return c;
                }
            }

            public T get<T>() where T : shard
            {
                if (stars.TryGetValue(typeof(T), out shard m))
                    return m as T;
                else
                    return null;
            }

            // local callback
            event Action<shard> _newShard = delegate { };
            public event Action<shard> InNewShard
            {
                add { _newShard += value; }
                remove { _newShard -= value; }
            }

            public class write
            {
                internal static write _o { private set; get; }
                IAuthor author;
                Dictionary <Type,shard> founder = new Dictionary<Type, shard> ();

                public write ( IAuthor _author )
                {
                    author = _author;
                }


                public write ( Type [] types, IAuthor _author )
                {
                    author = _author;

                    foreach ( var x in types )
                    RequireFounder ( x );
                }

                static internal T Querry <T> () where T : shard => _o.RequireFounder ( typeof (T) ) as T;

                shard RequireFounder ( Type type )
                {
                    if ( founder.ContainsKey ( type ) )
                        return founder [type];
                    else
                    {
                        var c = Activator.CreateInstance (type) as shard;
                        founder.Add ( type, c );
                        harmonize ( type );
                        return c;
                    }
                }

                void harmonize ( Type type )
                {
                    Type current = type;

                    if (!type.IsSubclassOf (typeof (shard)))
                    throw new InvalidOperationException ( "only shard type can be in a constelation" );
            
                    while ( current != typeof ( shard ) )
                    {
                        var fis = current.GetFields( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        foreach (var fi in fis)
                        {
                        if (fi.GetCustomAttribute<harmonyAttribute>() != null)
                            RequireFounder (fi.FieldType);
                        }
                        current = current.BaseType;
                    }
                }

                public constelation constelation ()
                {
                    _o = this;
                    author.writings ();
                    _o = null;

                    return new constelation ( founder );
                }
            }
        }
        


        public abstract class ink <T> where T : shard, new()
        {
            public ink()
            {
                if (constelation.write._o == null)
                    throw new InvalidOperationException("ink can only be instantied in Writings ()");

                o = constelation.write.Querry <T> ();
                o.ready = true;
            }

            /// <summary>
            /// target shard where the ink is written
            /// </summary>
            protected T o { private set; get; }
        }
    }

    public class ink <T> where T : shard, new()
    {
        public T o { private set; get; }

        public ink ()
        {
            if (shard.constelation.write._o == null)
                    throw new InvalidOperationException("ink can only be instantied in Writings ()");

            o = shard.constelation.write.Querry <T> ();
        }
    }


    /// <summary> category of a shard, For editor use </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class verseAttribute : Attribute
    {
        public string Name { get; }

        public verseAttribute(string Verse)
        { Name = Verse; }
    }

    /// <summary> mark this field for dependency injection </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class harmonyAttribute : Attribute
    { }

    /// <summary> mark this field to be editable on serialized shards </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class lyricAttribute : Attribute
    { }

    /// <summary> mark this shard to need external data ( ink ) </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class inkedAttribute : Attribute
    { }
}

// TODO: reflection cache for dependency