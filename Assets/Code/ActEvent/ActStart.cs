using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using static Pixify.treeBuilder;

namespace Triheroes.Code
{
    public static class Act
    {
        public static void Start (script script, Character c)
        {
            if (script != null)
            {
            m_character_controller mcc = c.ConnectNode ( new m_character_controller () );
            
            TreeStart ( c );
            mcc.StartRoot ( script.CreateTree ());
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
            Act.Start ( script, Director.o );
            // NOTE: ActStart script are not cleaned after they're finished, but this is negligible for this game, but will have to consider this for a future game

            Destroy (gameObject);
        }
    }
}
