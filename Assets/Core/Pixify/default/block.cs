using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    // TODO mains might include action that are added in script, have to make sure these are added after all depedences are added

    // alliance of component shard
    public class block
    {
        List <pix> bricks = new List<pix> ();
        Dictionary <Type,pix> mains = new Dictionary<Type, pix> ();

        public block ( Dictionary <Type,pix> FounderBricks )
        {
            bricks.AddRange ( FounderBricks.Values );

            foreach ( var kvp in FounderBricks )
                mains.Add (kvp.Key, kvp.Value);

            foreach ( var p in bricks )
            {
            typeof (pix).GetProperty (b, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ).SetValue ( p, this );
            p.Create ();
            }
        }

        const string b = "b";
        public void IntegratePix ( pix brick )
        {
            if (bricks.Contains (brick)) return;

            bricks.Add ( brick );

            if ( !mains.ContainsKey (brick.GetType ()) )
            mains.Add ( brick.GetType (), brick );

            Type current = brick.GetType ();
            while ( current != typeof ( pix ) )
            {
                var fis = current.GetFields( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var fi in fis)
                {
                if (fi.GetCustomAttribute<DependAttribute>() != null)
                    fi.SetValue ( brick, RequirePix(fi.FieldType) );
                }
                current = current.BaseType;
            }
            
            typeof (pix).GetProperty (b, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ).SetValue ( brick, this );
            
            brick.Create ();
            _NewMember ( brick );
        }

        pix RequirePix ( Type PixType )
        {
            if ( mains.ContainsKey ( PixType ) )
                return mains [PixType];
            else
            {
                var c = (pix) Activator.CreateInstance (PixType);
                IntegratePix ( c );
                return c;
            }
        }

        /// <summary>
        /// require pix in the fly,  don't use too much, use depend instead
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PixType"></param>
        /// <returns></returns>
        public T RequirePix <T> () where T:pix , new ()
        {
            if ( mains.ContainsKey ( typeof (T) ) )
                return mains [typeof (T) ] as T;
            else
            {
                var c = Activator.CreateInstance (typeof (T)) as T;
                IntegratePix ( c );
                return c;
            }
        }

        public bool HavePix <T> ()
        {
            return mains.ContainsKey ( typeof (T) );
        }

        public T GetPix <T> () where T:pix
        {
            if (mains.TryGetValue(typeof(T), out pix m))
                return m as T;
            else
                return null;
        }

        // ------- block callbacks ----------
        event Action <pix> _NewMember;
        public event Action <pix> OnNewMember
        {
            add { _NewMember += value; }
            remove { _NewMember -= value; }
        }
    }
}