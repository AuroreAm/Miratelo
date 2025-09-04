using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra.Spirit
{
    public class Overture : MonoBehaviour
    {
        public ActPaper Script;
        void Awake ()
        {
            act script = Script.GetAction ();
            phoenix.galaxy.add ( script );
            Act.Start ( script );
            Destroy (gameObject);
        }
    }
}