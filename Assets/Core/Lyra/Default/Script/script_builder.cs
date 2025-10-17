using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    public class script_builder {
        
        system s;
        List <action> bricks = new List<action> ();
        action root;
        
        public script_builder ( system _s ) {
            s = _s;
        }

        public T a <T> () where T : action, new () {
            T a = new T ();
            bricks.Add (a);

            decorator_content.Peek ().Add (a);

            return a;
        }

        Stack <decorator_kind> decorator_domain = new Stack <decorator_kind> ();
        Stack <List<action>> decorator_content = new Stack<List<action>> ();
        public T _a <T> () where T : action , decorator_kind, new () {
            T a = new T ();
            bricks.Add (a);

            decorator_domain.Push (a);
            decorator_content.Push ( new List<action> () );

            if ( root == null ) root = a;
            return a;
        }

        public void _ () {
            var d = decorator_domain.Pop ();
            d.set ( decorator_content.Pop ().ToArray () );
        }

        public action final () {
            foreach ( var b in bricks )
            s.add (b);

            return root;
        }
    }
}