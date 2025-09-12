using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class actor_sfx : moon
    {
        [link]
        character c;

        AudioSource au;

        protected override void _ready()
        {
            au = c.gameobject.AddComponent<AudioSource>();
            au.outputAudioMixerGroup = aeris.master;
            au.spatialBlend = 1;
        }

        public void play ( int name )
        {
            au.PlayOneShot ( game_resources.SE.q ( name ) );
        }
    }
}

