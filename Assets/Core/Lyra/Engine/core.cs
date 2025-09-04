using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace Lyra
{
    public class core : ICore
    {
        int [] melodymark;
        List<Type> melody;
        Dictionary < Type, Type > noteindex = new Dictionary<Type, Type> ();

        public core ( Type [] _melody )
        {
            melody = new List<Type> (_melody);
            melodymark = new int [_melody.Length];

            for (int i = 0; i < melody.Count; i++)
            {
                noteindex.Add ( melody [i], melody [i] );
                
                if (melody [i].GetCustomAttribute<noteAttribute>() != null)
                {
                    List <Type> deriveds = new List <Type> ();

                    foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                     deriveds.AddRange ( a.GetTypes().Where(type => type.IsSubclassOf(melody [i]) ) );

                    foreach ( var a in deriveds )
                    noteindex.Add ( a, melody [i] );
                }
            }
        }

        List <aria> song = new List<aria> ();
        
        Dictionary < aria, List <aria.flow> >  chords = new Dictionary<aria, List<aria.flow>> ();

        Queue < List < aria.flow > > notepool = new Queue<List<aria.flow>> ();
        List < aria.flow > RentChord () => notepool.Count > 0 ? notepool.Dequeue () : new List<aria.flow> ();
        void ReturnChord ( List <aria.flow> a ) { a.Clear (); notepool.Enqueue (a); }

        public void start ( aria s )
        {
            if ( song.Contains (s) )
            {
                Debug.LogError ( "this aria is already started, make sure it's stopped before starting it" );
                return;
            }

            if ( ! noteindex.ContainsKey ( s.GetType () ) )
            {
                Debug.LogError ( $"this aria cannot be processed, the type {s.GetType().Name} is missing in the execution order" );
                return;
            }

            Type note = noteindex [s.GetType ()];
            int AddingAdress = melodymark [ melody.IndexOf (note) ];

            song.AddAtIndex ( AddingAdress, s );

            for (int i = melody.IndexOf ( note ) + 1; i < melodymark.Length; i++)
            melodymark [i] ++;

            chords.Add ( s, RentChord () );

            s.sing (this);
        }

        /// <summary> tell the core that this sys is started but not executed by this processor, this is used so that this aria can link another </summary>
        public void fakestart ( aria s )
        {
            chords.Add ( s, RentChord () );
        }

        public void fakestop ( aria s )
        {
            foreach ( aria link in chords [s] )
                link.halt (this);

            ReturnChord ( chords [s] );
            chords.Remove (s);
        }

        public void link ( aria host, aria.flow link )
        {
            chords [host].Add ( link );
            start (link);
        }

        public void unlink ( aria host, aria.flow link )
        {
            chords [host].Remove (link);
            link.halt ( this );
        }

        public void inhalt(aria s)
        {
            if ( !song.Contains (s) )
            {
                Debug.LogError ( "this sys is already stopped" );
                return;
            }

            int address = song.IndexOf ( s );
            song.RemoveAt (address);

            foreach ( aria link in chords [s] )
                link.halt (this);

            ReturnChord ( chords [s] );
            chords.Remove (s);
            
            for (int i = 0; i < melodymark.Length; i++)
            {
                if ( melodymark [i] > address )
                melodymark [i] --;
            }
        }

        aria [] _stackCache;
        public void pulse ()
        {
            if (_stackCache == null || _stackCache.Length <= song.Count)
            _stackCache = new aria [ song.Count + 200 ];

            int length = song.Count;

            for (int i = 0; i < song.Count; i++)
                _stackCache [i] = song [i];

            for (int i = 0; i < length; i++)
            if (_stackCache [i].on)
            _stackCache [i].sing (this);
        }
    }

    [AttributeUsage(AttributeTargets.Class,Inherited = false)]
    public class noteAttribute : Attribute
    {
        public int pitch {get; private set;}

        public noteAttribute ( int _pitch )
        {
            pitch = _pitch;
        }
    }

    public static class ariaExtensions
    {
        /// <summary>
        /// start another aria and link with this, the linked aria stops when the host is stopped
        /// </summary>
        public static void link ( this aria s, aria.flow link )
        {
            phoenix.core.link ( s, link );
        }

        public static void unlink ( this aria s, aria.flow link )
        {
            phoenix.core.unlink ( s, link );
        }
    }
}