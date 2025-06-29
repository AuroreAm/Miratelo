using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class Element : ModulePointer<m_element>
    {
        public static Element o;

        public Element ()
        {
            o = this;
        }

        public static bool ElementActorIsNotAlly ( int id, int selfFaction )
        {
            if ( ! o.ptr.ContainsKey (id) ) return false;
            if ( o.ptr[id].ma.faction != selfFaction ) return true;
            return false;
        }

        public static void SendMessage <T> (  int to, int message, T context ) where T:struct
        {
            o.ptr[to].SendMessage (message, context);
        }

        public static void Clash ( element from, int to, Slash force )
        {
            o.ptr[to].element.Clash ( from, force );
        }

        public static void Clash ( element from, int to, Perce force )
        {
            o.ptr[to].element.Clash ( from, force );
        }

        public static void Clash ( element from, int to, Knock force )
        {
            o.ptr[to].element.Clash ( from, force );
        }

    }


    public interface IELBFC
    {}
    public interface IElementListener <T> : IELBFC where T : struct
    {
        public void OnMessage (int message, T context);
    }

    public class m_element : moduleptr <m_element>
    {
        [Depend]
        public m_actor ma;
        public element element { private set; get; }

        List <IELBFC> elementListeners = new List<IELBFC> ();

        public void LinkMessage ( IELBFC listener )
        {
            elementListeners.Add (listener);
        }

        public void UnlinkMessage (IELBFC listener)
        {
            elementListeners.Remove (listener);
        }

        public void SendMessage <T> (int message, T context) where T:struct
        {
            foreach (var i in elementListeners)
            (i as IElementListener<T>) ? .OnMessage (message, context);
        }

        public void SetElement ( element e )
        {
            character.ConnectNode ( e );
            element = e;
        }
    }

    public abstract class element : node
    {
        // clash from another element
        public virtual void Clash ( element from, Slash force )
        {}

        public virtual void Clash ( element from, Perce force )
        {}

        public virtual void Clash ( element from, Knock force )
        {}
    }

    public static class MessageKey
    {
        public static readonly SuperKey hooked_up = new SuperKey ("hooked_up");
        public static readonly SuperKey knocked_out = new SuperKey ("knocked_out");
    }
}