using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class Element : PixIndex<s_element>
    {
        public static Element o;

        public Element ()
        {
            o = this;
        }

        public static bool ElementActorIsNotAlly ( int id, int selfFaction )
        {
            if ( ! o.ptr.ContainsKey (id) ) return false;
            if ( o.ptr[id].da.faction != selfFaction ) return true;
            return false;
        }

        public static void SendMessage <T> (  int to, T context ) where T:struct
        {
            o.ptr[to].SendMessage (context);
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
        public void OnMessage ( T context );
    }

    public class s_element : character_indexed_pix <s_element>, IElementContainer
    {
        [Depend]
        public d_actor da;
        public element element { private set; get;}

        List <IELBFC> elementListeners = new List<IELBFC> ();

        public void LinkMessage ( IELBFC listener )
        {
            elementListeners.Add (listener);
        }

        public void UnlinkMessage (IELBFC listener)
        {
            elementListeners.Remove (listener);
        }

        public void SendMessage <T> ( T context ) where T:struct
        {
            foreach (var i in elementListeners)
            (i as IElementListener<T>) ? .OnMessage ( context);
        }

        public void SetElement (element e)
        {
            element = e;
            e.Link(this);
        }
    }

    public interface IElementContainer 
    {
        public void SendMessage <T> ( T context ) where T:struct;
    }

    public abstract class element : pix
    {
        // clash from another element
        public virtual void Clash ( element from, Slash force )
        {}

        public virtual void Clash ( element from, Perce force )
        {}

        public virtual void Clash ( element from, Knock force )
        {}

        protected IElementContainer host;

        public void Link ( IElementContainer container )
        {
            host = container;
        }
    }

    public static class Signal
    {
        public static readonly term damage = new term ("damage");
        public static readonly term hooked_up = new term ("hooked_up");
        public static readonly term knocked_out = new term ("knocked_out");

        public static readonly term incomming_slash = new term ("incomming_slash");
    }
}