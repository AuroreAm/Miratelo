using Lyra;
using UnityEngine;

namespace Triheroes.Code.CapsuleAct
{
    public class jump : act
    {
        public override int priority => level.action;
        public override bool accept2nd => true;

        [link]
        capsule capsule;
        [link]
        skin skin;

        delta_curve cu;
        float max;
        float min;
        bool done;

        public term jump_animation = animation.jump;

        protected override void _ready ()
        {
            cu = new delta_curve ( triheroes_res.curve.q ( animation.jump ).curve );
        }

        public void set ( float _max, float _min )
        {
            max = _max;
            min = _min;
        }

        protected override void _start()
        {
            this.link(capsule);

            cu.start(max, .5f);
            skin.play( new skin.animation ( jump_animation, this ) );
            done = false;
        }
        
        
        /// <param name="dir">per second</param>
        public void move(Vector3 dir, float factor = walk_factor.run)
        {
            if (on)
                capsule.dir += dir * Time.deltaTime * factor;
        }

        public void stop_jump()
        {
            if ( cu.current >= min && cu.current < (max + min)/ 2 )
            done = true;
        }

        protected override void _step()
        {
            capsule.dir += new Vector3(0, cu.tick_delta (), 0);

            if ( done )
            {
                stop ();
                return;
            }

            if (cu.done)
                stop();
        }
    }
}
