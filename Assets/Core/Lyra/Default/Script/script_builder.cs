using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    public class sb {
        
        system s;
        List <action> bricks = new List<action> ();
        action root;
        
        public static sb _a_all ( system _s ) {
            var sb = new sb ( _s );
            sb._a <parallel.all> ();
            return sb;
        }

        public sb ( system _s ) {
            s = _s;
        }

        public T a <T> () where T : action, new () {
            T a = new T ();
            bricks.Add (a);

            decorator_content.Peek ().Add (a);

            return a;
        }

        Stack <decorator> decorator_domain = new Stack <decorator> ();
        Stack <List<action>> decorator_content = new Stack<List<action>> ();
        public T _a <T> () where T : action , decorator, new () {
            T a = new T ();
            bricks.Add (a);

            decorator_domain.Push (a);
            decorator_content.Push ( new List<action> () );

            if ( root == null ) root = a;
            return a;
        }

        public void _ () {
            var d = decorator_domain.Pop ();
            d.contract.set_childs ( decorator_content.Pop ().ToArray () );
        }

        public action result () {
            foreach ( var b in bricks )
            s.add (b);

            return root;
        }
    }
}