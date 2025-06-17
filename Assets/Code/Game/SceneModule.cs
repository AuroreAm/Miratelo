using System;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    
    [Serializable]
    public struct SceneData
    {
        public string NomDeLaCarte;
        public string BGMNatif;
    }

    public class MapId : module
    {
        public override void Create()
        {
            o = this;
        }
        public static MapId o;
        public SceneData Scene;
    }

}
