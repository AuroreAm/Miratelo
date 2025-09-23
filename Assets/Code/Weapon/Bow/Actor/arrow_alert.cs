using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class arrow_alert : controller, gold<incomming_arrow>
    {
        [link]
        character c;

        public float timeleft { private set; get; }
        public bool alert { private set; get; }
        public Vector3 position { private set; get; }
        public float speed { private set; get; }

        protected override void _start()
        {
            clear ();
        }

        protected override void _stop()
        {
            clear ();
        }

        void clear ()
        {
            timeleft = Mathf.Infinity;
            alert = false;
        }

        public void _radiate( incomming_arrow gleam )
        {
            var _timeleft = Vector3.Distance ( c.position, gleam.position ) / gleam.speed;

            if ( timeleft > _timeleft )
            {
                alert = true;
                frame = true;
                timeleft = _timeleft;
                position = gleam.position;
                speed = gleam.speed;
            }
        }

        bool frame;
        protected override void _step()
        {
            if ( !frame && alert )
            clear ();

            if (frame)
            frame = false;
        }
    }
}
