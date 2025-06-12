using Pixify;
using System;
using System.Collections.Generic;

namespace Triheroes.Code
{
    public class TriheroesMighty : PixifyEngine
    {
        public override void BeforeCreateSystems()
        {
            gameObject.AddComponent <Vecteur> ();
            gameObject.AddComponent <ActorFaction> ();
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
                new p_slash_attack.s_slash_attack(),

                // stats

                // character movement
                new s_ccc_gravity (),
                new CoreSystem<m_capsule_character_controller>(),

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