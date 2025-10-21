using Lyra;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class face_idle : action {
        [link]
        face_animation face;

        const int idle_start = 4;
        const int idle_length = 6;

        int state;
        float t;

        protected override void _step() {
            if ( state == 0 )
            t -= Time.deltaTime;

            if ( state == 0 && t <= 0 ) {
                t = 0;
                state = Mathf.FloorToInt ( Random.Range ( 1, 3 ) );
            }

            switch ( state ) {
                case 1 :
                face.set_image_index ( idle_start + Mathf.FloorToInt ( Random.Range (0, idle_length) ) );

                t = Random.Range ( 1, 3 );
                state = 0;

                break;

                case 2 :

                if ( t >= 3 ) {
                    face.set_image_index ( idle_start + Mathf.FloorToInt ( Random.Range (0, idle_length) ) );
                    
                    t = Random.Range ( 1, 5 );
                    state = 0;

                    return;
                }

                t += Time.deltaTime * 8;
                face.set_image_index ( Mathf.FloorToInt (t) );

                break;
            }
        }
    }
}
