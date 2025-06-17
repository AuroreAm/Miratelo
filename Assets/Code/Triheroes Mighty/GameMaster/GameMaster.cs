using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using System;

namespace Triheroes.Code
{
    // TODO: remove execution order of this class when the menu is done
    public class GameMaster : MonoBehaviour
    {
        // TODO: create game menu to start the game
        public Game New;

        void Awake ()
        {
            ResourcesLoad ();
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

        // TODO: remove character instantiation from here and put it in the scene master, but need a way to persist character data

        void Start ()
        {
            LoadGame (New);
        }
        
        void ResourcesLoad ()
        {
            SubResources <AudioClip>.LoadAll ( "BGM" );
            SubResources <AudioClip>.LoadAll ( "SE" );
            SubResources <ParticleSystem>.LoadAll ( "Spectre" );
            SubResources <TrailRenderer>.LoadAll ( "Spectre" );
        }
    }

    public sealed class GameData
    {
        public static GameData o;
        public m_actor [] MainActors;
        public Character GetMainCharacters(int i) => MainActors[i].character;
        public int MainCharacter;
    }
}