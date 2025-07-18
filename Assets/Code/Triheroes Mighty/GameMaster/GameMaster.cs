using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using System;
using Codice.Client.Common;
using System.IO;

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
            LoadGame (New);
        }

        void LoadGame (Game game)
        {
            new GameData ( game );
        }
        
        void ResourcesLoad ()
        {
            SubResources <VirtusAuthor>.LoadAll ( "Virtus" );
            SubResources <AudioClip>.LoadAll ( "BGM" );
            SubResources <AudioClip>.LoadAll ( "SE" );
            SubResources <ParticleSystem>.LoadAll ( "Spectre" );
            SubResources <TrailRenderer>.LoadAll ( "Spectre" );
            SubResources <CurveRes>.LoadAll ("Path");
        }
    }

    public sealed class GameData
    {
        public static GameData o;

        public GameData (Game Model)
        {
            o = this;
            LoadedGame = ScriptableObject.Instantiate(Model);
        }

        public Game LoadedGame;
    }
}