using Lyra;
using UnityEngine;

namespace Triheroes.Code
{

    [path ("ai")]
    public class choose_point_001_left_right_of_target : action {
        [link]
        point_001 point;

        [link]
        warrior warrior;

        [export]
        public float distance = 5;

        float angle;

        protected override void _start() {
            int choice = Random.Range (0, 2);

            switch (choice) {
                case 0: angle = -90f; break;
                case 1: angle = 90f; break;
                default: angle = 0f; break;
            }

            calculate_pos ();

            stop ();
        }

        public void calculate_pos () {
            point.pos = vecteur.ldir (angle + vecteur.rot_direction_y ( warrior.c.position, warrior.target.c.position ), Vector3.forward * distance ) + warrior.target.c.position;
        }
    }
}