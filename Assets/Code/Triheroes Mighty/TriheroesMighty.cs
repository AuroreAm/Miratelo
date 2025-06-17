using Pixify;
using System;
using System.Collections.Generic;

namespace Triheroes.Code
{
    [Serializable]
    public struct SceneData
    {
        public string NomDeLaCarte;
        public string BGMNatif;
    }

    public class TriheroesMighty : PixifyEngine
    {
        public SceneData Scene;

        public override void BeforeCreateSystems()
        {
            Director d = gameObject.AddComponent <Director> ();
            
            d.RequireModule <Vecteur> ();
            d.RequireModule <ActorFaction> ();
            d.RequireModule <BGMMaster> ();
        }

        protected override void CreateSystems(out List<PixifySytemBase> systems)
        {
            // Game main systems
            systems = new List<PixifySytemBase> ()
            {
                // character physic datas
                new s_ccc_ground_data(),

                // procedural animations
                new CoreSystem<m_skin_procedural>(),

                // character behavior and controller
                new CoreSystem<m_character_controller>(),
                new CoreSystem<controller>(),

                // attacks
                new s_slash_attack(),
                new s_trajectile (),

                // stats

                // character movement
                new s_ccc_gravity (),
                new CoreSystem<m_capsule_character_controller>(),

                // elements reaction
                new s_element (),

                // character skin and animations
                new CoreSystem<m_skin>(),

                // camera
                new CoreSystem<m_camera_controller>()

                // tweener

                // Audio
            };
        }
    }
}