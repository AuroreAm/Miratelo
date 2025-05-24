using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Pixify
{
    public class ActionModel : Node, IComparable<ActionModel>
    {
        public String Tag;

        [Input]
        int In;

        [SerializeField]
        public NodePaper BluePrintPaper;

        // instancied nodes from ActionModels are executed from top to bottom
        public int CompareTo(ActionModel other)
        {
            if (position.y < other.position.y) return -1; else return 1;
        }

        /// <summary>
        /// Create node using the blueprint and connect it to the character
        /// </summary>
        public virtual action CreateNode (Character c)
        {
            var n = BluePrintPaper.CreateNode() as action;
            n.Tag = new SuperKey(Tag);
            c.ConnectNode ( n );
            return n;
        }
    }
}