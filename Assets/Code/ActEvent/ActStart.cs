using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public static class Act
    {
        public static void Start (action ScriptRoot)
        {
            if (ScriptRoot != null)
            {

            neuron n = catom.New <neuron> ( ScriptRoot.character );
            n.Aquire ( new atom (), ScriptRoot );

            }
            else
            Debug.LogWarning ("script is null");
        }
    }

    // start a script at the beginning of the scene
    public class ActStart : MonoBehaviour
    {
        public script script;
        void Awake ()
        {
            Act.Start ( script.WriteTree ( Director.o ) );
            // NOTE: ActStart script are not cleaned after they're finished, but this is negligible for this game, but will have to consider this for a future game

            Destroy (gameObject);
        }
    }
}
