using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra.Spirit
{
    public class ActStart : MonoBehaviour
    {
        public ActionPaper Script;
        void Awake ()
        {
            action script = Script.GetAction ();
            SceneMaster.Master.Add ( script );
            Act.Start ( script );
            Destroy (gameObject);
        }
    }
}