using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra.Spirit
{
    public class ActStart : MonoBehaviour
    {
        public Script script;
        void Awake ()
        {
            Act.Start ( script.WriteTree ( Stage.Director ) );
            Destroy (gameObject);
        }
    }
}
