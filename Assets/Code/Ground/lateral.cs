using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;

namespace Triheroes.Code
{
    public class lateral : act
    {
        public override priority priority => priority.def3.with2nd();

        [link]
        axeal a;

        [link]
        skin skin;
        [link]
        ground ground;
        [link]
        stand stand;

        term state;
        Vector3 dir;

        bool firstframe;

        protected override void _start ()
        {
            stand.use(this);
            if (firstframe == true)
            {
                dir = Vector3.zero;
                to_idle();
                firstframe = false;
            }
            else
            {
                if (state == animation.idle)
                    to_idle();
                else to_lateral();
            }
        }

        protected override void _step()
        {
            set_animation();
            rotation();
            dir = Vector3.zero;
        }

        void set_animation()
        {
            // idle => lateral
            if (state == animation.idle && dir.sqrMagnitude >= 0.01f)
            {
                to_lateral();
                set_animation();
                return;
            }
            // lateral => idle // lateral animation
            else if (state == animation.run_lateral)
            {
                Vector3 relativeDir = vecteur.ldir (360 - skin.roty, dir).normalized;
                set_animation_direction_float(relativeDir.x, relativeDir.z);

                if (dir.sqrMagnitude < 0.01f)
                    to_idle();
            }
        }

        void rotation()
        {
            stand.rotate_skin ();
        }

        void to_lateral()
        {
            skin.play( new skin.animation ( animation.run_lateral, this ) );
            state = animation.run_lateral;
        }

        void to_idle()
        {
            skin.play ( new skin.animation ( animation.idle, this ) );
            state = animation.idle;
        }

        float dx; float dz;
        void set_animation_direction_float (float _dx, float _dz, float GravityPerSecond = 3)
        {
            dx = Mathf.MoveTowards(dx, _dx, GravityPerSecond * Time.deltaTime);
            dz = Mathf.MoveTowards(dz, _dz, GravityPerSecond * Time.deltaTime);

            skin.ani.SetFloat(hash.dx, dx);
            skin.ani.SetFloat(hash.dz, dz);
        }
        
        /// <param name="dir_s"> per second </param>
        public void walk_lateral(Vector3 dir_s)
        {
            if (on)
            {
                a.walk (  dir_s  );
                this.dir += dir_s;
            }
        }
    }
}
