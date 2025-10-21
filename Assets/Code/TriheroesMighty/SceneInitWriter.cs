using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class SceneInitWriter : MonoBehaviour {
        public void Awake () {
            game._scene_start += Init;
        }

        protected abstract void Init ();
    }
}