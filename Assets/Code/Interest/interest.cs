using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [superstar]
    public class codex : index <interest> {
        List <interest> list = new List<interest> ();

        protected override void _new(int id, interest pix) {
            list.Add ( pix );
        }

        public interest near_to ( Vector3 pos, float distance ) {
            interest current = null;

            foreach ( var i in list ) {
                float distance_of_i = Vector3.Distance ( pos, i.pos );
                if ( distance_of_i < distance ) {
                    distance = distance_of_i;
                    current = i;
                }
            }

            return current;
        }
    }

    public abstract class interest : public_moon <interest> {
        public abstract Vector3 pos {get;}
    }
}