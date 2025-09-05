using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// aquire for autoplay
    /// </summary>
    public class footstep : controller
    {
        // INPROGRESS
        /*[Depend]
        e_foot ef;

        // TODO: not use the overall character position, but the foot position
        [Depend]
        d_dimension dd;

        bool autoplay;
        // TODO: double interval for accuracy
        float interval;
        float time;
        int ground;

        protected sealed override void Step()
        {
            if (autoplay)
            {
                time += Time.deltaTime;
                if (time > interval)
                {
                    PlayFootstep ();
                    time = 0;
                }
            }
        }

        public void Play ( float interval )
        {
            autoplay = true;
            this.interval = interval;
            time = 0;
        }

        protected override void Stop()
        {
            StopFootStep ();
        }

        public void StopFootStep ()
        {
            autoplay = false;
        }

        public void PlayFootstep ()
        {
            // fetch the ground element
            if ( Physics.Raycast ( dd.position + Vector3.up * .1f, Vector3.down, out RaycastHit hit, .5f, Vecteur.Solid ) )
            ground = hit.collider.id ();

            if ( GroundElement.Contains ( ground ) )
            GroundElement.Clash ( ef, ground );
        }*/
    }
}
