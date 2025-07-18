using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{

    public class number : pix
    {
        public int value;
    }

    public class add_number : task
    {
        [Depend]
        number number;

        protected override void Start()
        {
            number.value ++;
            SelfStop ();
        }
    }

    public class log_number: task
    {
        [Depend]
        number number;

        public int n;

        public Notion notion;

        public log_number ()
        {
            notion = new Notion (Problem, new term ("zero"));
        }

        bool Problem ()
        {
            if ( number.value < n )
            {
                notion.Solution = new term ("add");
                return false;
            }

            notion.Solution = new term ("zero");
            return true;
        }

        public log_number (int _n)
        {
            n = _n;
        }

        protected override void Step()
        {
            Debug.Log ( number.value );
            SelfStop ();
        }

    }
}
