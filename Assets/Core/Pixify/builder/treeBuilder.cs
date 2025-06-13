using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Pixify
{
    public static class treeBuilder
    {

        static bool Initialized;
        static List<action> Heap = new List<action>();
        static Stack<decorator> DecoratorStack = new Stack<decorator>();
        static Stack<List<action>> DecoratorChildStack = new Stack<List<action>>();
        static Character c;
        static action root;

        public static void TreeStart(Character owner)
        {
            if (Initialized)
                throw new InvalidOperationException("fatal error, treeStart without finalizing the previous");
            c = owner;
            Initialized = true;
            
            Heap.Clear();
            DecoratorStack.Clear();
            root = null;
        }

        public static void Write(node node)
        {
            if (!Initialized)
                return;

            if (node is action a)
            {
                if (root == null)
                    root = a;

                if (a.GetType().GetCustomAttribute<UniqueAttribute>() != null && c.uniques.TryGetValue(a.GetType(), out action b))
                    a = b;
                else
                {
                    Heap.Add(a);
                    if (a.GetType().GetCustomAttribute<UniqueAttribute>() != null)
                        c.uniques.Add(a.GetType(), a);
                }

                if (DecoratorChildStack.Count > 0)
                    DecoratorChildStack.Peek().Add(a);

                if (node is decorator d)
                {
                    DecoratorStack.Push(d);
                    DecoratorChildStack.Push(new List<action>());
                }
            }
        }

        public static void end()
        {
            DecoratorStack.Pop().o = DecoratorChildStack.Pop().ToArray();
        }

        public static action TreeFinalize()
        {
            if (!Initialized)
                throw new InvalidOperationException("fatal error, tree finalize without tree start");

            foreach (var a in Heap)
                c.ConnectNode(a);

            Initialized = false;
            Heap.Clear();
            DecoratorStack.Clear();

            return root;
        }

    }
}