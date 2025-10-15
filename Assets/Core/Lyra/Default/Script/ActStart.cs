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
            action script = Script.write ( phoenix.star.get <script.crypt> () );
            action.start ( script );
            Destroy (gameObject);
        }
    }
}