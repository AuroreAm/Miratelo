using Lyra;
using Triheroes.Code.Axeal;
using Triheroes.Code.Mecha;
using UnityEngine;

namespace Triheroes.Code {
    public class mecha_dash_slay : slay_base, gold<parried> {
        const float vu_per_mu = .02f;

        [link]
        mecha_sword mecha_sword;

        [link]
        axeal a;

        [link]
        stun stun;

        force_curve_data f;
        term animation = Code.animation.SS4;

        protected override sword weapon => (sword) mecha_sword;

        protected override void _ready() {
            f = new force_curve_data ( skin.duration(animation) - skin.event_points(animation)[0], res.curves.q ( new term ( "jump" ) ) );
        }

        protected override void __start() {
            skin.play(
                new skin.animation(animation, this)
                {
                    end = stop,
                    ev0 = dash,
                    ev1 = begin_slash
                });

            send_slash_signal();
        }

        void begin_slash()
        {
            weapon.slash ( weapon.matter.mu * vu_per_mu, paths.paths[animation], skin.duration(animation) - skin.event_points(animation)[1] );
            // vecteur.ldir (skin.roty, new Vector3 ( 0,1,1 ) * 10 ) * 10, 10
        }

        void dash () {
            f.dir = vecteur.ldir ( skin.roty, Vector3.forward ) * 5f;
            a.set_force (f);
        }

        void send_slash_signal()
        {
            RaycastHit[] ToSendSignal;
            ToSendSignal = Physics.SphereCastAll(skin.position, weapon.length, vecteur.ldir(skin.roty, Vector3.forward), 6, vecteur.Character);

            foreach (RaycastHit hit in ToSendSignal)
            {
                if (pallas.contains(hit.collider.id()) && pallas.is_enemy(hit.collider.id(), warrior.faction))
                    pallas.radiate(hit.collider.id(),
                    new incomming_slash(
                        ((actor)warrior).term,
                        animation,
                        skin.event_points ( animation ) [1] )  );
            }
        }
        
        [link]
        motor motor;
        public void _radiate(parried gleam) {
            motor.start_act (stun);
        }
    }
}