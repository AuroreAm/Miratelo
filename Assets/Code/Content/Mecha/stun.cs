using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    public class stun : act
    {
        public override priority priority => priority.reaction;

        [link]
        skin skin;

        float time;
        const float duration = 3;

        protected override void _start()
        {
            time = duration;
            skin.play ( new skin.animation ( anim.stun_begin, this ) {end = stun_hold} );
        }

        void stun_hold ()
        {
            skin.play ( new skin.animation ( anim.stun, this ) );
        }

        protected override void _step()
        {
            if ( time <= 0 )
            stop ();

            time -= Time.deltaTime;
        }
    }
}