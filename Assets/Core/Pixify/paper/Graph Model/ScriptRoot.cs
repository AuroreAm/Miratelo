using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class ScriptRoot : XNode.Node
    {
        [Output]
        public int Out;

        public ActionModel GetRoot()
        {
            return GetPort("Out").Connection.node as ActionModel;
        }
    }
}