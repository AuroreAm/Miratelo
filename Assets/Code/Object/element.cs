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

    public interface IElementListener
    {
        public void OnMessage (int message);
    }

    public class m_element : moduleptr <m_element>
    {
        [Depend]
        public m_actor ma;
        public element element { private set; get; }

        List <IElementListener> elementListeners = new List<IElementListener> ();

        public void LinkMessage ( IElementListener listener )
        {
            elementListeners.Add (listener);
        }

        public void UnlinkMessage (IElementListener listener)
        {
            elementListeners.Remove (listener);
        }

        public void SendMessage (int message)
        {
            foreach (var i in elementListeners)
            i.OnMessage (message);
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
        public static readonly SuperKey knock_forced = new SuperKey ("knock_forced");
    }
}