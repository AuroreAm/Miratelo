using Pixify;
using UnityEngine;
using UnityEngine.Audio;

namespace Triheroes.Code
{
    // sfx for UI and master for every SFX
    public class SFXMaster : module
    {
        public static SFXMaster o;
        public static AudioMixerGroup MainMixer;
        AudioSource AuUI;

        public override void Create()
        {
            o = this;
            SubResources <AudioMixer>.LoadAll ( "SE" );
            MainMixer = SubResources<AudioMixer>.q(new SuperKey("MainMixer")).FindMatchingGroups ("Master") [0];

            AuUI = character.gameObject.AddComponent<AudioSource>();
            AuUI.spatialBlend = 0;

            // add SFX pool
            UnitPoolMaster.AddPool ( new SfxAuthor (), "SFX" );
        }

        class SfxAuthor : IUnitAuthor
        {
            public Unit Instance()
            {
                Unit u = new Unit ();
                u.RequirePiece<p_sfx> ();
                u.Create ();
                return u;
            }
        }

        public void PlayUISFX ( int name )
        {
            AuUI.PlayOneShot ( SubResources<AudioClip>.q ( name ) );
        }
    }

    public class p_sfx : piece
    {
        static readonly SuperKey SFX = new SuperKey("SFX");
        AudioSource Au;
        public override void Create()
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
            UnitPoolMaster.GetUnit ( SFX );
        }

        protected override void OnStart()
        {
            Au.clip = _clip;
            Au.transform.position = _pos;
            Au.Play ();
        }

        public override void Main()
        {
            if (!Au.isPlaying)
                unit.Return_();
        }
    }
}