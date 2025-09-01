using Lyra;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Triheroes.Code
{
    // sfx for UI and master for every SFX
    public class SFXMaster : pix
    {
        public static SFXMaster o;
        public static AudioMixerGroup MainMixer;
        AudioSource AuUI;

        public override void Create()
        {
            o = this;
            SubResources <AudioMixer>.LoadAll ( "SE" );
            MainMixer = SubResources<AudioMixer>.q(new term("MainMixer")).FindMatchingGroups ("Master") [0];

            AuUI = Stage.o.gameObject.AddComponent<AudioSource>();
            AuUI.spatialBlend = 0;

            // add SFX pool
            VirtualPoolMaster.AddPool ( new SfxAuthor (), "SFX" );
        }

        class SfxAuthor : IVirtusAuthor, IBlockAuthor
        {
            public void OnWriteBlock()
            {}

            virtus IVirtusAuthor.Instance()
            {    
                List <Type> PixTypes = new List<Type> ();
                PixTypes.A <virtus> ();
                PixTypes.A <a_sfx> ();
                var b = new PreBlock ( PixTypes.ToArray (), this ).CreateBlock ();
                return b.GetPix <virtus> ();
            }
        }

        public void PlayUISFX ( int name )
        {
            AuUI.PlayOneShot ( SubResources<AudioClip>.q ( name ) );
        }
    }

    public class a_sfx : virtus.pixi
    {
        static readonly term SFX = new term ("SFX");
        AudioSource Au;
        protected override void Create1()
        {
            Au = new GameObject("SFX Channel").AddComponent<AudioSource>();
            Au.spatialBlend = 1;
            Au.outputAudioMixerGroup = SFXMaster.MainMixer;
        }

        static AudioClip _clip;
        static Vector3 _pos;
        public static void Play ( int name, Vector3 pos )
        {
            _clip = SubResources<AudioClip>.q ( name );
            _pos = pos;
            VirtualPoolMaster.RentVirtus ( SFX );
        }

        protected override void Start()
        {
            Au.clip = _clip;
            Au.transform.position = _pos;
            Au.Play ();
        }

        protected override void Step()
        {
            if (!Au.isPlaying)
                v.Return_();
        }
    }
}