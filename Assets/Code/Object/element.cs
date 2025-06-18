using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("Object")]
    public class element_writer : ModuleWriter
    {
        public override void WriteModule(Character character)
        {
            var me = character.RequireModule<m_element>();
            me.SetElement ( new e_skin () );
        }
    }

    public class m_element : moduleptr <m_element>
    {
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
        public abstract void Clash ( element from, Force force );
        // response to a clash from this element
        public abstract void ReverseClash ( element from, Force force );
    }
}