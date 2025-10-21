using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public class ActStart : MonoBehaviour
    {
        public ActionPaper Script;
        void Start ()
        {
            action script = Script.Write ( phoenix.core );
            action.start ( script );
            Destroy (gameObject);
        }
    }
}