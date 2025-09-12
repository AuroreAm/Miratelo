using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.Audio;

namespace Triheroes.Code
{

    [superstar]
    public class aeris : moon
    {
        public static aeris o;
        public static AudioMixerGroup master;
        AudioSource au_ui;

        protected override void _ready()
        {
            o = this;
            master = Resources.Load <AudioMixer> ( "SE/MainMixer" ).FindMatchingGroups ("Master") [0];

            au_ui = new GameObject().AddComponent<AudioSource>();
            au_ui.name = "UI SFX";
            au_ui.spatialBlend = 0;

            // add SFX pool
            orion.add ( new sfx_creator (), "SFX" );
        }
        
        class sfx_creator : virtus_creator_simple
        {
            protected override void _virtus_creation()
            {
                new Lyra.ink <sfx> ();
            }
        }

        public void play ( int name )
        {
            au_ui.PlayOneShot ( game_resources.SE.q ( name ) );
        }
    }

    public class sfx : virtus.star
    {
        static readonly term SFX = new term ("SFX");
        AudioSource au;

        protected override void __ready()
        {
            au = new GameObject("SFX Channel").AddComponent<AudioSource>();
            au.spatialBlend = 1;
            au.outputAudioMixerGroup = aeris.master;
        }

        static AudioClip _clip;
        static Vector3 _pos;
        public static void play ( int name, Vector3 pos )
        {
            _clip = game_resources.SE.q ( name );
            _pos = pos;
            orion.rent ( SFX );
        }

        protected override void _start ()
        {
            au.clip = _clip;
            au.transform.position = _pos;
            au.Play ();
        }

        protected override void _step ()
        {
            if (!au.isPlaying)
                virtus.return_();
        }
    }
}