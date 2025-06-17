using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class GameMaster : MonoBehaviour
    {
        // TODO: create game menu to start the game
        public Game New;

        void Awake ()
        {
            ResourcesLoad ();
            LoadGame (New);
        }

        void LoadGame (Game game)
        {
            GameData.o = new GameData ();

            // TODO: load map

            // load players
            GameData.o.MainActors = new m_actor [game.ActivePartyMembers.Length];
            for (int i = 0; i < game.ActivePartyMembers.Length; i++)
            {
                GameData.o.MainActors[i] = game.ActivePartyMembers[i].Spawn ( game.ActivePartyMembersPosition[i], Quaternion.Euler(game.ActivePartyMembersRotation[i]) ).RequireModule <m_actor> ();
            }
        }
        
        void ResourcesLoad ()
        {
            SubResources <AudioClip>.LoadAll ( "BGM" );
        }
    }

    public sealed class GameData
    {
        public static GameData o;
        public m_actor [] MainActors;
        public int MainCharacter;
    }
}