using Lyra;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Triheroes.Code
{
    [CreateAssetMenu (fileName = "Game", menuName = "Game/Game")]
    public class GameData : ScriptableObject {
        ActorAuthor [] main_actors;
    }
}