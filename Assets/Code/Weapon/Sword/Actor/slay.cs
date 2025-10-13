using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public struct hack {
        public float raw;
        public Vector3 position;

        public hack(float _raw, Vector3 _position) {
            raw = _raw;
            position = _position;
        }
    }

    public abstract class slay_base : act {
        public override priority priority => priority.action;

        [link]
        protected slay.skin_path paths;
        [link]
        protected skin_dir skin_dir;
        [link]
        protected warrior warrior;
        [link]
        protected actor actor;
        [link]
        protected skin skin;

        protected abstract sword weapon { get; }

        protected sealed override void _start() {
            link(skin_dir);
            skin_dir.dir = skin.ani.transform.forward;
            __start();
        }

        protected virtual void __start() { }
    }

    public class slay : slay_base {

        const float vu_per_mu = .005f;

        [link]
        sword_user sword_user;

        term animation;

        protected override sword weapon => sword_user.weapon;

        public slay(term _animation) {
            animation = _animation;
        }

        protected override void __start() {
            skin.play(new skin.animation(animation, this) { end = stop, ev0 = begin_slash });
            send_slash_signal();
        }

        void begin_slash() {
            weapon.slash ( weapon.matter.mu * vu_per_mu , paths.paths[animation], skin.duration(animation) - skin.event_points(animation)[0] );
        }

        void send_slash_signal() {
            Collider[] nearby;
            nearby = Physics.OverlapSphere(weapon.position, weapon.length, vecteur.Character);

            foreach (Collider col in nearby) {
                if (pallas.contains(col.id()) && pallas.is_enemy(col.id(), warrior.faction))
                    pallas.radiate(col.id(), new incomming_slash(actor.term, animation, skin.event_points(animation)[0]));
            }
        }

        public class skin_path : moon {
            public Dictionary<term, path> paths = new Dictionary<term, path>();
        }

        [Serializable]
        public class path {
            public term key;
            // time interval between two points
            public const float delta = 0.005f;
            public Vector3[] orig;
            public Vector3[] dir;
        }
    }

    public struct incomming_slash {
        public term sender;
        public term slash;
        public float duration;

        public incomming_slash(term sender, term slash, float duration) {
            this.sender = sender;
            this.slash = slash;
            this.duration = duration;
        }
    }
}