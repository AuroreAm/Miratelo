using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [Serializable]
    public class ActionModel
    {
        public String Tag;
        public bool Valid => BluePrintPaper.Valid;

        [SerializeField]
        public NodePaper BluePrintPaper;

        public ActionModel ()
        {
            BluePrintPaper = new NodePaper();
        }

        /// <summary>
        /// Create node using the blueprint and connect it to the character
        /// </summary>
        public virtual action CreateNode (Character c)
        {
            if ( BluePrintPaper.IsUnique && DecoratorModel.UniqueNodes != null && DecoratorModel.UniqueNodes.TryGetValue (BluePrintPaper.Type, out node n1) )
            return (action) n1;

            var n = BluePrintPaper.CreateNode() as action;
            n.Tag = new SuperKey(Tag);
            c.ConnectNode ( n );

            if ( BluePrintPaper.IsUnique )
                DecoratorModel.UniqueNodes.Add (BluePrintPaper.Type, n);

            return n;
        }

        #if UNITY_EDITOR
        /// <summary>
        /// Create a copy of the node
        /// </summary>
        public virtual ActionModel Copy()
        {
            var n = new ActionModel();
            n.Tag = Tag;
            n.BluePrintPaper = BluePrintPaper.Copy();
            return n;
        }
        #endif
    }
}