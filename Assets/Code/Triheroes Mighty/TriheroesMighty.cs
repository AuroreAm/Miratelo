using Pixify;
using Pixify.Spirit;
using System;
using System.Collections.Generic;

namespace Triheroes.Code
{
    public class TriheroesMighty : Stage
    {
        public SceneData Scene;
        public Script MapStartScript;

        public override void SetDirectorPix( in List <Type> a )
        {
            
            a.A <Player> ();
            a.A <VirtualPoolMaster> ();
            a.A <Vecteur> ();
            a.A <ActorList> ();
            a.A <BGMMaster> ();
            a.A <MapId> ();
            a.A <SFXMaster> ();
            a.A <Spectre> ();
            a.A <Element> ();
            a.A <GroundElement> ();
            a.A <Interactable> ();

        }

        protected override Type[] PixiExecutionOrder()
        {
            return new Type []
            {
                typeof ( bios ),

                typeof ( s_ground_data_ccc ),

                typeof ( s_skin_procedural ),

                typeof ( action ),
                typeof ( spirit ),
                typeof ( reflexion ),
                typeof ( controller ),

                typeof ( a_slash_attack ),
                typeof ( a_trajectile ),
                typeof ( a_t_explosive ),

                typeof ( s_stat_generator ),

                typeof ( s_gravity_ccc ),
                typeof ( s_capsule_character_controller ),

                typeof ( s_skin ),

                typeof ( camera_shot ),
                typeof ( s_camera ),

                typeof ( graphic_frame ),
                typeof ( m_tween ),

                typeof ( a_sfx )
            };
        }

        public override void StageStart()
        {
            Act.Start ( MapStartScript.WriteTree ( Director ) );
        }
    }
}