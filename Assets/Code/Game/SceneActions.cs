using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify.Spirit;
using Pixify;

namespace Triheroes.Code
{
    [Category ("bios")]
    public class g_bios_use_play : action
    {
        [Depend]
        m_game_bios mgb;
        [Depend]
        play play;

        protected override void Start ()
        {
            mgb.Set (play);
            SelfStop ();
        }
    }

    //INPROGRESS
    /*
    [Category ("bios")]
    public class g_bios_use_ingame_cinematic : action
    {
        [Depend]
        m_game_bios mgb;
        [Depend]
        in_game_cinematic ingame_cinematic;

        protected override bool Step ()
        {
            mgb.Set (ingame_cinematic);
            return true;
        }
    }*/

    [Category ("bios")]
    public class g_playBGM : action
    {
        [Depend]
        BGMMaster BGM;

        public int BGMName;

        protected override void Start ()
        {
            BGM.PlayBGM ( BGMName );
            SelfStop ();
        }
    }
}
