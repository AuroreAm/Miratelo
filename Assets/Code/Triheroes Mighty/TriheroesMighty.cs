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

        protected override void CreateSystems(out List<CoreSystemBase> systems)
        {
            // Game main systems
            systems = new List<CoreSystemBase> ()
            {
                // character physic datas
                new s_ccc_ground_data(),

                // procedural animations

                // character behavior and controller
                new CoreSystem<m_character_controller>(),
                new CoreSystem<controller>(),

                // attacks

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