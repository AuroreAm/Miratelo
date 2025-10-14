using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// link for autoplay
    /// </summary>
    public class footstep : controller
    {
        [link]
        foot foot;

        // TODO: not use the overall character position, but the foot position
        [link]
        actor actor;

        bool autoplay;
        // TODO: double interval for accuracy
        float interval;
        float time;
        int ground;

        protected sealed override void _step()
        {
            if (autoplay)
            {
                time += Time.deltaTime;
                if (time > interval)
                {
                    play ();
                    time = 0;
                }
            }
        }

        public void play ( float interval )
        {
            autoplay = true;
            this.interval = interval;
            time = 0;
        }

        public void play ()
        {
            // fetch the ground element
            if ( Physics.Raycast ( actor.position + Vector3.up * .1f, Vector3.down, out RaycastHit hit, .5f, vecteur.Solid ) )
            ground = hit.collider.gameObject.GetInstanceID ();

            if ( terra.contains ( ground ) )
            terra.clash ( foot, ground );
        }

        protected override void _stop ()
        {
            stop ();
        }

        public void stop ()
        {
            autoplay = false;
        }

    }
}
