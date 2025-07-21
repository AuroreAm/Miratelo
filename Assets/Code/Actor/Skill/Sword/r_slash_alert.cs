using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify.Spirit;
using Pixify;

namespace Triheroes.Code
{
    public class r_slash_alert : reflexion, IElementListener<incomming_slash>
    {
        [Depend]
        s_element se;

        protected override void Start()
        {
            se.LinkMessage (this);   
        }

        protected override void Stop()
        {
            se.UnlinkMessage (this);
        }

        public void OnMessage(incomming_slash context)
        {
            Debug.Log ( context.sender.name + "is planning a slash" );
        }
    }
}
