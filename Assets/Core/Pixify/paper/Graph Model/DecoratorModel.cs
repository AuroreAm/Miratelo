using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class DecoratorModel : ActionModel
    {
        [SerializeReference]
        public List <ActionModel> Child;

        public static Dictionary <Type, node> UniqueNodes {get; private set;}

        public DecoratorModel ()
        {
            Child = new List<ActionModel>();
        }

        public override action CreateNode(Character c)
        {
            // set the uniques nodes dictionary to call by the next child models where CreateNode is called
            // can only be called once, once the dictionary is set, next decorator models can't set it again because they are child
            if (UniqueNodes == null)
                UniqueNodes = new Dictionary<Type, node>();

            // create the nodes from the child models
            List <action> _o = new List<action> ();
            foreach (var n in Child)
                _o.Add (n.CreateNode(c));

            // remove the dictionary
            UniqueNodes = null;

            // create the decorator and add the created nodes from the child
            var d = BluePrintPaper.CreateNode() as decorator;
            d.Tag = new SuperKey(Tag);
            d.o = _o.ToArray();
            c.ConnectNode (d);
            return d; 
        }

        #if UNITY_EDITOR
        public override ActionModel Copy()
        {
            var n = new DecoratorModel();
            n.Tag = Tag;
            n.BluePrintPaper = BluePrintPaper.Copy();

            n.Child = new List<ActionModel>();
            foreach (var c in Child)
                n.Child.Add(c.Copy());
            return n;
        }
        #endif
    }
}