using System.Collections;
using System.Collections.Generic;
using Pixify;
using static Pixify.treeBuilder;
using UnityEngine;

namespace Triheroes.Code
{
    // bios for the main gameplay
    // host for character in scene
    // play a character and let AI move the others in party
    // switch character
    public class play : bios
    {
        public static play o;

        public override void Create()
        {
            o = this;
            SpawnMainCharacters ();
        }

        m_actor [] MainActors;
        int MainActor;
        action [] MainPlayerScripts;
        m_HP [] MainPlayerHP;
        m_ie [] MainPlayerIE;

        public static Character MainCharacter => o.MainActors[o.MainActor].character;
        public static Character GetMainCharacter(int i) => o.MainActors[i].character;
        public static int ActorCount => o.MainActors.Length;

        void SpawnMainCharacters()
        {
            MainActors = new m_actor[ GameData.o.LoadedGame.ActivePartyMembers.Length ];

            for (int i = 0; i < GameData.o.LoadedGame.ActivePartyMembers.Length; i++)
            {
                MainActors[i] = GameData.o.LoadedGame.ActivePartyMembers[i].Spawn ( GameData.o.LoadedGame.ActivePartyMembersPosition[i], Quaternion.Euler(GameData.o.LoadedGame.ActivePartyMembersRotation[i]) ).RequireModule <m_actor> ();
            }
        }

        protected override void OnAquire()
        {
            MainPlayerScripts = new action[ MainActors.Length ];
            MainPlayerHP = new m_HP[ MainActors.Length ];
            MainPlayerIE = new m_ie[ MainActors.Length ];

            for (int i = 0; i < MainPlayerScripts.Length; i++)
            {
                TreeStart ( MainActors[i].character );
                MainPlayerScripts[i] = PlayerControllerLibrary.MainPlayer ();

                MainPlayerHP[i] = MainActors[i].character.RequireModule<m_HP>();
                MainPlayerIE[i] = MainActors[i].character.RequireModule<m_ie>();
            }

            SetMainPlayer ( MainActor );
        }

        void SetMainPlayer ( int i )
        {
            MainActors[i].character.RequireModule <m_character_controller> ().StartRoot ( MainPlayerScripts[i] );
            // set the player hud
            gf_player_hud.o.SetIdentity ( MainPlayerHP[i].MaxHP, MainPlayerHP[i].HP, MainPlayerIE[i].MaxIE, MainPlayerIE[i].IE,  MainActors[i].md );
            // set the camera
            m_camera.o.TpsACharacter ( MainActors[i].md );

            MainActor = i;
        } 

        public override void Main()
        {
            // show stats to UI
            gf_player_hud.o.SetCurrentHP ( MainPlayerHP[MainActor].HP );
            gf_player_hud.o.SetCurrentIE ( MainPlayerIE[MainActor].IE );
            
        }
    }
}
