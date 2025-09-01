using System;
using System.Collections.Generic;
using Lyra.Spirit;
using Lyra;

namespace Triheroes.Code
{
    
    [Serializable]
    public struct SceneData
    {
        public string NomDeLaCarte;
        public string BGMNatif;
    }

    public class MapId : pix
    {
        public override void Create()
        {
            o = this;
        }
        public static MapId o;
        public SceneData Scene;
    }

    
    [Category ("graphic frame")]
    public class gf_show_map_title : action
    {
        protected override void Start()
        {
            gf_title.SetTitleText ( TriheroesTitle.MapTitle, MapId.o.Scene.NomDeLaCarte );
            gf_title.ShowTitle ( TriheroesTitle.MapTitle );
            SelfStop ();
        }
    }

}
