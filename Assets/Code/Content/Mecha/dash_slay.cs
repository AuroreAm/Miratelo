using Lyra;
using System.Collections;
using System.Collections.Generic;
using Triheroes.Code.Mecha;
using Triheroes.Code.Sword;
using Triheroes.Code.Sword.Combat;
using UnityEngine;

/*
namespace Triheroes.Code
{
     
    public class mecha_slay : act
    {
        
        public override priority priority => priority.action;

        [link]
        actor actor;
        [link]
        warrior warrior;
        [link]
        capsule capsule;
        [link]
        skin_dir skin_dir;
        [link]
        skin skin;
        [link]
        slay.skin_path paths;
        
        [link]
        mecha_sword mecha_sword;

        term animation;

        public mecha_slay ( term _animation )
        {
            animation = _animation;
        }

        protected override void _start ()
        {
            link (capsule);
            link (skin_dir);
            skin_dir.dir = skin.ani.transform.forward;
            skin.play ( new skin.animation ( animation, this ) { end = stop, ev0 = begin_slash } );
            send_slash_signal();
        }

        void begin_slash ()
        {
            slash.fire ( ((sword)mecha_sword).slash, ((sword)mecha_sword), paths.paths [animation], skin.duration (animation) - skin.event_points (animation) [0] );
        }

        void send_slash_signal ()
        {
            Collider[] nearby;
            nearby = Physics.OverlapSphere ( ((sword)mecha_sword).position, ((sword)mecha_sword).length, vecteur.Character );

            foreach (Collider col in nearby)
            {
                if ( pallas.contains (col.id()) && pallas.is_enemy ( col.id(), warrior.faction ) )
                    pallas.radiate (col.id(), new incomming_slash ( actor.term, animation, skin.event_points (animation) [0] ) );
            }
        }
    }

    public class dash_slay : act, gold <parried>
    {
        public override priority priority => priority.action;

        [link]
        warrior warrior;
        [link]
        skin skin;
        [link]
        mecha_sword mecha_sword;
        [link]
        dimension dimension;
        [link]
        slay.skin_path path;
        [link]
        capsule capsule;
        [link]
        motor motor;
        [link]
        react_hook react_hook;

        int state;

        readonly term slash_animation = animation.SS4;

        protected override void _ready()
        {
            cu = new delta_curve(triheroes_res.curve.q(animation.jump).curve);
        }

        protected override void _start()
        {
            link(capsule);
            state = 0;
            skin.play(
                new skin.animation(slash_animation, this)
                {
                    end = stop,
                    ev0 = dash,
                    ev1 = begin_slash
                });

            send_slash_signal();
        }

        protected override void _step()
        {
            if (state == 1)
                capsule.dir += vecteur.ldir(skin.roty, Vector3.forward) * cu.tick_delta();
            
        }

        delta_curve cu;
        void dash()
        {
            cu.start(5, skin.duration(slash_animation) - skin.event_points(slash_animation)[0]);
            state = 1;
        }

        void begin_slash()
        {

            var slash_name = ((sword)mecha_sword).slash;
            knocker.fire(slash_name, (sword)mecha_sword, path.paths[slash_animation], skin.duration(slash_animation) - skin.event_points(slash_animation)[1],  vecteur.ldir (skin.roty, new Vector3 ( 0,1,1 ) * 10 ) * 10, 10);
            // slash.fire(slash_name, (sword)mecha_sword, path.paths[slash_animation], skin.duration(slash_animation) - skin.event_points(slash_animation)[1] );
        }

        void send_slash_signal()
        {
            RaycastHit[] ToSendSignal;
            ToSendSignal = Physics.SphereCastAll(skin.position, ((sword)mecha_sword).length, vecteur.ldir(skin.roty, Vector3.forward), 6, vecteur.Character);

            foreach (RaycastHit Hit in ToSendSignal)
            {
                if (pallas.contains(Hit.collider.id()) && pallas.is_enemy(Hit.collider.id(), warrior.faction))
                    pallas.radiate(Hit.collider.id(),
                    new incomming_slash(
                        ((actor)warrior).term,
                        slash_animation,
                        skin.event_points ( slash_animation ) [1] )  );
            }
        }

        [link]
        stun stun;

        public void _radiate( parried gleam )
        {
            motor.start_act (stun);
        }
    }
}
*/