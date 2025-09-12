using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public class ActStart : MonoBehaviour
    {
        public ActionPaper Script;
        void Awake ()
        {
            action script = Script.write ();
            phoenix.star.add ( script );
            action.start ( script );
            Destroy (gameObject);
        }
    }
}