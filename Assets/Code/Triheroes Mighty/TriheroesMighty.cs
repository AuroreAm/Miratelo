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

            d.RequireModule <Player> ();

            d.RequireModule <UnitPoolMaster> ();
            d.RequireModule <Vecteur> ();
            d.RequireModule <ActorFaction> ();
            d.RequireModule <BGMMaster> ();
            d.RequireModule <MapId> ().Scene = Scene;
            d.RequireModule <SFXMaster> ();
            d.RequireModule <Spectre> ();

            d.RequireModule <Element> ();
            d.RequireModule <GroundElement> ();
            d.RequireModule <Interactable> ();
        }

        public override void AfterCreateSystems()
        {
            Act.Start ( MapStartScript.WriteTree ( Director.o ) );
        }

        protected override void CreateSystems(out List<PixifySytemBase> systems)
        {
            // Game main systems
            systems = new List<PixifySytemBase> ()
            {
                // game bios
                new IntegralSystem<bios>(),

                // character physic datas
                new s_ground_data_ccc(),

                // procedural animations
                new IntegralSystem<m_skin_procedural>(),

                // character behavior and controller
                new IntegralSystem<reflection>(),
                new IntegralSystem<neuron>(),
                new IntegralSystem<m_character_controller>(),
                new IntegralSystem<controller>(),

                // attacks
                new IntegralSystem<p_slash_attack>(),
                new IntegralSystem<p_trajectile>(),

                // stats
                new IntegralSystem<m_stat_generator>(),

                // character movement
                new IntegralSystem<m_gravity_mccc>(),
                new IntegralSystem<m_capsule_character_controller>(),

                // character skin and animations
                new IntegralSystem<m_skin>(),

                // camera
                new IntegralSystem<camera_shot>(),
                new IntegralSystem<m_camera>(),

                // UI
                new IntegralSystem<graphic_frame>(),
                new IntegralSystem<m_tween>(),

                // Audio
                new IntegralSystem<p_sfx>()
            };
        }
    }
}