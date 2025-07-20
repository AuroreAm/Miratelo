using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class PointerPaper : ThoughtAuthor<pointer>
    {
        public string to;
        protected override pointer Get(block b)
        {
            var t = new pointer ( new term ( to ) );
            b.IntegratePix ( t );
            return t;
        }
    }
}
