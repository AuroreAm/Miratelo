using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [Category("decorator")]
    [NodeTint(1,0,0)]
    public abstract class decorator : action {
        // TODO: verify if exposing this to public will cause future problems
        public action [] o;

        static action [] _o;

        public static decorator New ( Type t, ICatomFactory factory, action [] childs )
        {
            _o = childs;
            return New <decorator> ( t, factory );
        }

        public static decorator New ( string AssemblyQualifiedType, string decoratorData, ICatomFactory factory, action [] childs )
        {
            _o = childs;
            return New <decorator> ( AssemblyQualifiedType, decoratorData, factory );
        }

        public decorator ()
        {
            o = _o;
            _o = null;

            if (o == null)
            throw new InvalidProgramException ("A decorator was created without child initialization, use New<T> of decorator to create an instance of a decorator");
        }
    }
}
