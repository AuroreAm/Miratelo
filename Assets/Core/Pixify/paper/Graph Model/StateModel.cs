using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class state : action
    {
        // dummy placeholder for state model
        public ScriptModel State;
    }

    [Serializable]
    public class StateModel : ActionModel
    {
        ScriptModel Script;

        public StateModel()
        {
            if (Script)
            ((state) BluePrintPaper.blueprint).State = Script;
        }

        public void Set(ScriptModel s)
        {
            Script = s;
            Tag = s.Root.Tag;
            BluePrintPaper.Set (typeof(state));
            ((state) BluePrintPaper.blueprint).State = Script;
        }

        override public action CreateNode (Character c)
        {
            var o = Script.Root.CreateNode(c);
            o.Tag = new SuperKey(Tag);
            return o;
        }
    }
}