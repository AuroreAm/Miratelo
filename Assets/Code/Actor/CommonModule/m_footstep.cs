using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// aquire for autoplay
    /// </summary>
    [RegisterAsBase]
    public class m_footstep : controller
    {
        [Depend]
        e_foot ef;

        // TODO: not use the overall character position, but the foot position
        [Depend]
        m_dimension md;

        bool autoplay;
        // TODO: double interval for accuracy
        float interval;
        float time;
        int ground;

        public sealed override void Main()
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

        protected override void OnFree()
        {
            Stop ();
        }

        public void Stop ()
        {
            autoplay = false;
        }

        public void PlayFootstep ()
        {
            // fetch the ground element
            if ( Physics.Raycast ( md.position + Vector3.up * .1f, Vector3.down, out RaycastHit hit, .5f, Vecteur.Solid ) )
            ground = hit.collider.id ();

            if ( GroundElement.GroundExist ( ground ) )
            GroundElement.Clash ( ef, ground );
        }
    }
}
