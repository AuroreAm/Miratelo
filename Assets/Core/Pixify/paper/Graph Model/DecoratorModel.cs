using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class DecoratorModel : ActionModel
    {
        [Output]
        int Out;

        public override action CreateNode(Character c)
        {
            // fetch all child model
            List <ActionModel> Child = new List<ActionModel>();
            var Connections = GetPort("Out").GetConnections();
            for (int i = 0; i < Connections.Count; i++)
                Child.Add ( Connections[i].node as ActionModel);
            
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
    }
}