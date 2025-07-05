using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [Category("debug")]
    public class Log : action
    {
        [Export]
        public string log;

        protected override bool Step()
        {
            Debug.Log (log);
            return true;
        }
    }
    
    [Category("debug")]
    public class Skip : action
    {
        protected override bool Step()
        {
            return true;
        }
    }
    
    [Category("debug")]
    public class Void : action
    {
        protected override bool Step()
        {
            return false;
        }
    }

}
