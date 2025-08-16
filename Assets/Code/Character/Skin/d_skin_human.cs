using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class d_hand : pix
    {
        public Transform [] Hand {private set; get;}

        public class package : PreBlock.Package <d_hand>
        {
            public package ( Transform [] hand  )
            {
                o.Hand = hand;
            }
        }
    }
}