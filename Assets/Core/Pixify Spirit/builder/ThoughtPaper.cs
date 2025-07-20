using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class ThoughtPaper : ThoughtAuthor <thought.package>
    {
        public PixPaper <thought.package> paper;

        protected override thought.package Get(block b)
        {
                var t = paper.Write ();
                b.IntegratePix ( t );
                return t;
        }
    }
}
