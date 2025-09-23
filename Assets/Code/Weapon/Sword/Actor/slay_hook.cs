/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using static Triheroes.Code.Sword.Combat.slay;

namespace Triheroes.Code.Sword.Combat
{
    public class slay_hook_up : act
    {
        public override priority priority => priority.action;

        [link]
        actor actor;
        [link]
        sword_user sword_user;
        [link]
        skin skin;
        [link]
        skin_path paths;
        [link]
        capsule capsule;

        term animation;
        delta_curve cu;

        readonly float duration = .5f;
        readonly float jumpHeight = 2;


        public slay_hook_up ( term _animation )
        {
            animation = _animation;
        }

        protected override void _ready()
        {
            cu = new delta_curve ( triheroes_res.curve.q ( new term ("jump") ).curve );
        }

        protected override void _start ()
        {
            link (capsule);

            cu.start (jumpHeight, duration);

            skin.play ( new skin.animation ( animation, this ) { end = stop, ev0 = begin_slash } );
            send_slash_signal();
        }

        protected override void _step()
        {
            capsule.dir += cu.tick_delta () * Vector3.up;
        }

        void begin_slash ()
        {
            hooker.fire ( sword_user.weapon.slash_hook_up, sword_user.weapon, paths.paths [animation], skin.duration (animation) - skin.event_points (animation) [0], cu.curve, Vector3.up * jumpHeight, duration );
        }

        void send_slash_signal ()
        {
            Collider[] nearby;
            nearby = Physics.OverlapSphere ( sword_user.weapon.position, sword_user.weapon.length, vecteur.Character );

            foreach (Collider col in nearby)
            {
                if ( pallas.contains (col.id()) )
                    pallas.radiate (col.id(), new incomming_slash ( actor.term, animation, skin.event_points (animation) [0] ) );
            }
        }
    }

    public class slay_hook_spam : act
    {
        
        public override priority priority => priority.action;

        [link]
        actor actor;
        [link]
        sword_user sword_user;
        [link]
        skin skin;
        [link]
        skin_path paths;

        term animation;

        public slay_hook_spam ( term _animation )
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
            slash.fire ( sword_user.weapon.slash_hook_spam, sword_user.weapon, paths.paths [animation], skin.duration (animation) - skin.event_points (animation) [0] );
        }

        void send_slash_signal ()
        {
            Collider[] nearby;
            nearby = Physics.OverlapSphere ( sword_user.weapon.position, sword_user.weapon.length, vecteur.Character );

            foreach (Collider col in nearby)
            {
                if ( pallas.contains (col.id()) )
                    pallas.radiate (col.id(), new incomming_slash ( actor.term, animation, skin.event_points (animation) [0] ) );
            }
        }
    }

}
*/