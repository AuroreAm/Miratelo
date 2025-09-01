using System.Collections;
using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    // start a script when the main character enters a trigger
    public class ActTrigger : MonoBehaviour
    {
        public Script script;
        action triggerRoot;

        void Awake ()
        {
            triggerRoot = script.WriteTree ( Stage.Director );
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.id() == play.o.MainActor.c.gameObject.GetInstanceID())
            {
                Act.Start ( triggerRoot );
                Destroy (gameObject);
            }
        }
    }
}