using Pixify;
using System;
using System.Collections.Generic;

namespace Triheroes.Code
{
    public class TriheroesMighty : PixifyEngine
    {
        public SceneData Scene;
        public script MapStartScript;

        public override void BeforeCreateSystems()
        {
            Director d = gameObject.AddComponent <Director> ();

            d.RequireModule <UnitPoolMaster> ();
            d.RequireModule <Vecteur> ();
            d.RequireModule <ActorFaction> ();
            d.RequireModule <BGMMaster> ();
            d.RequireModule <MapId> ().Scene = Scene;
            d.RequireModule <SFXMaster> ();
            d.RequireModule <Spectre> ();

            d.RequireModule <Element> ();
            d.RequireModule <GroundElement> ();
        }

        public override void AfterCreateSystems()
        {
            Act.Start ( MapStartScript, Director.o );
        }

        protected override void CreateSystems(out List<PixifySytemBase> systems)
        {
            // Game main systems
            systems = new List<PixifySytemBase> ()
            {
                // game bios
                new CoreSystem<bios>(),

                // character physic datas
                new s_ccc_ground_data(),

                // procedural animations
                new CoreSystem<m_skin_procedural>(),

                // character behavior and controller
                new CoreSystem<m_character_controller>(),
                new CoreSystem<controller>(),

                // attacks
                new PieceSystem<p_slash_attack>(),
                new PieceSystem<p_trajectile>(),

                // effects
                new s_trail_spectre (),

                // stats
                new CoreSystem<m_stat_generator>(),

                // character movement
                new s_ccc_gravity (),
                new CoreSystem<m_capsule_character_controller>(),

                // character skin and animations
                new CoreSystem<m_skin>(),

                // camera
                new CoreSystem<m_camera_controller>(),

                // tweener

                // Audio
                new s_sfx ()
            };
        }
    }
}