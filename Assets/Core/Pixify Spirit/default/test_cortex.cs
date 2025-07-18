using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    [Category("test")]
    public class test_cortex : cortex
    {
        [Depend]
        s_mind sm;

        [Depend]
        test_reflexion tr;

        public override void Create()
        {
            for (int i = 0; i < 100; i++)
            {
                var l = new log_number ();
                l.n = i;
                sm.AddNotion ( new plan_notion ( l.notion, l, new term (i.ToString ()) ) );
            }
            sm.AddNotion ( new plan_notion ( new add_number (), new term ("add") ) );
        }

        public override void Think ()
        {
            sm.AddReflexion ( tr );
        }
    }

    public class test_reflexion : reflexion
    {
        int n;
        
        [Depend]
        s_mind sm;

        protected override void Step()
        {
            if ( Input.GetKeyDown(KeyCode.A) )
            sm.DoTask (new term ("99"));
        }
    }
}
