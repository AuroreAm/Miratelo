using Lyra;
using System.Collections;
using System.Collections.Generic;
using Triheroes.Code.Sword;
using Triheroes.Code.Sword.Combat;
using UnityEngine;

namespace Triheroes.Code
{
    public class dash_slay : act
    {
        public override int priority => level.action;

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

        int state;

        readonly term slash_animation = animation.SS4;

        protected override void _ready()
        {
            cu = new delta_curve(triheroes_res.curve.Q(animation.jump).curve);
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
            send_slash_signal();

            var slash_name = ((sword)mecha_sword).slash;
            slash.fire(slash_name, (sword)mecha_sword, path.paths[slash_animation], skin.duration(slash_animation) - skin.event_points(slash_animation)[1]);
        }

        void send_slash_signal()
        {
            RaycastHit[] ToSendSignal;
            ToSendSignal = Physics.SphereCastAll(skin.position, dimension.r, vecteur.ldir(skin.roty, Vector3.forward), 5, vecteur.Character);

            foreach (RaycastHit Hit in ToSendSignal)
            {
                if (pallas.contains(Hit.collider.id()) && pallas.is_enemy(Hit.collider.id(), warrior.faction))
                    pallas.radiate(Hit.collider.id(), new incomming_slash(
                        ((actor)warrior).term,
                        slash_animation));
            }
        }
    }
}
