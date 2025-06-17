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

        public Action<ClashEvent> OnClash { private set; get; }

        public void Clash ( element from, Force force )
        {
            element.Clash ( this, from, force );
        }

        public void RegisterOnClash ( Action<ClashEvent> action )
        {
            OnClash += action;
        }

        public void UnregisterOnClash ( Action<ClashEvent> action )
        {
            OnClash -= action;
        }

        public void SetElement ( element e )
        {
            character.ConnectNode ( e );
            element = e;
        }
    }

    public struct ClashEvent
    {
        public Force force;
        public element from;
        public HitType hitType;

        public ClashEvent ( Force force, element from, HitType hitType )
        {
            this.force = force;
            this.from = from;
            this.hitType = hitType;
        }
    }

    public enum HitType
    {
        NotSpecified,
        Small,
        Knockback
    }

    public abstract class element : node
    {
        public abstract void Clash ( m_element host, element from, Force force );
    }
}