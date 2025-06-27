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

        public Element ()
        {
            o = this;
        }
    }

    public class m_element : moduleptr <m_element>
    {
        [Depend]
        public m_actor ma;
        public element element { private set; get; }

        /// <summary>
        /// called when the element changes state
        /// </summary>
        /// <param name="state">the new state hash</param>
        Action <int> StateChange;
        Action <int> Message;
        public int State { private set; get; }

        public void SendMessage ( int message )
        {
            Message?.Invoke ( message );
        }

        public void SetState ( int state )
        {
            State = state;
            StateChange?.Invoke ( state );
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
    }
}