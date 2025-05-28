using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class DecoratorModel : ActionModel
    {
        [SerializeReference]
        public List <ActionModel> Child;

        public DecoratorModel ()
        {
            Child = new List<ActionModel>();
        }

        public override action CreateNode(Character c)
        {
            // create the nodes from the child models
            List <action> _o = new List<action> ();
            foreach (var n in Child)
                _o.Add (n.CreateNode(c));

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