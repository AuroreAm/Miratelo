using UnityEngine;
using Pixify;

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
        
        // TODO Refactor this as sometimes loading the resources is forgotten
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