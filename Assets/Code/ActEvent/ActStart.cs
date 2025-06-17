using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using static Pixify.treeBuilder;

namespace Triheroes.Code
{
    // start a script at the beginning of the scene
    public class ActStart : MonoBehaviour
    {
        public script script;
        void Awake ()
        {
            m_character_controller mcc = Director.o.ConnectNode ( new m_character_controller () );
            
            TreeStart ( Director.o );
            mcc.root = script.CreateTree ();
            
            mcc.Aquire (new Void ());
            // NOTE: ActStart script are not cleaned after they're finished, but this is negligible for this game, but will have to consider this for a future game

            Destroy (gameObject);
        }
    }
}
