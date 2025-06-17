using System.Collections;
using System.Collections.Generic;
using Pixify;
using static Pixify.treeBuilder;
using UnityEngine;

namespace Triheroes.Code
{
    // bios for the main gameplay
    // play a character and let AI move the others in party
    // switch character
    [RegisterAsBase]
    public class play : bios
    {
        action [] MainPlayerScripts;

        protected override void OnAquire()
        {
            MainPlayerScripts = new action[ GameData.o.MainActors.Length ];

            for (int i = 0; i < MainPlayerScripts.Length; i++)
            {
                TreeStart ( GameData.o.GetMainCharacters(i) );
                MainPlayerScripts[i] = PlayerControllerLibrary.MainPlayer ();
            }

            SetMainPlayer ( GameData.o.MainCharacter );
        }

        void SetMainPlayer ( int i )
        {
            GameData.o.GetMainCharacters ( i ).RequireModule <m_character_controller> ().StartRoot ( MainPlayerScripts[i] );
        }

        public override void Main()
        {}
    }
}
