using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    [Unique]
    public class g_bios_use_play : action
    {
        [Depend]
        m_game_bios mgb;
        [Depend]
        play play;

        protected override bool Step ()
        {
            mgb.Set (play);
            return true;
        }
    }

    [Unique]
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
    }

    public class g_playBGM : action
    {
        [Depend]
        BGMMaster BGM;

        public int BGMName;

        protected override bool Step ()
        {
            BGM.PlayBGM ( BGMName );
            return true;
        }
    }
}
