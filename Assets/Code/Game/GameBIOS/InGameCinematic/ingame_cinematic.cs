using System.Collections;
using System.Collections.Generic;
using Pixify;
using static Pixify.treeBuilder;
using UnityEngine;

namespace Triheroes.Code
{
    [RegisterAsBase]
    public class in_game_cinematic : bios
    {
        protected override void OnAquire()
        {
            for (int i = 0; i < GameData.o.MainActors.Length; i++)
            {
                TreeStart ( GameData.o.GetMainCharacters(i) );
                GameData.o.GetMainCharacters(i).RequireModule <m_character_controller> ().StartRoot ( PlayerCinematicActorLibrary.Dummy () );
            }
        }

        public override void Main()
        {}
    }
}
