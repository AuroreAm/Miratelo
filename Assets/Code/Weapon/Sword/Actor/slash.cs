using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public struct slash
    {
        public float raw;

        public slash ( float _raw )
        {
            raw = _raw;
        }
    }
}

namespace Triheroes.Code.Sword.Combat
{
    public class slash : act
    {
        public override int priority => level.action;

        [link]
        actor actor;
        [link]
        sword_user sword_user;
        [link]
        skin skin;
        [link]
        skin_path paths;

        term animation;

        public slash ( term _animation )
        {
            animation = _animation;
        }

        protected override void _start ()
        {
            skin.play ( new skin.animation ( animation, this ) { end = stop, ev0 = begin_slash } );
            send_slash_signal();
        }

        void begin_slash ()
        {
            Sword.slash.fire ( sword_user.weapon.slash, sword_user.weapon, paths.paths [animation], skin.duration (animation) - skin.event_points (animation) [0] );
        }

        void send_slash_signal ()
        {
            Collider[] nearby;
            nearby = Physics.OverlapSphere ( sword_user.weapon.position, sword_user.weapon.length, vecteur.Character );

            foreach (Collider col in nearby)
            {
                if ( pallas.contains (col.id()) )
                    pallas.radiate (col.id(), new incomming_slash ( actor.term, animation ) );
            }
        }

        public class skin_path : moon
        {
            public Dictionary < term, path > paths = new Dictionary < term, path > ();
        }

        [Serializable]
        public class path
        {
            public term key;
            // time interval between two points
            public const float delta = 0.005f;
            public Vector3[] orig;
            public Vector3[] dir;
        }
    }

    public struct incomming_slash
    {
        public term sender;
        public term slash;

        public incomming_slash( term sender, term slash )
        {
            this.sender = sender;
            this.slash = slash;
        }
    }
}