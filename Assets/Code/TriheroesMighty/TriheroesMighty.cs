using System.Collections;
using System.Collections.Generic;
using Lyra;

namespace Triheroes.Code
{
    public static class order
    {
        public const int capsule_ground_detection = -70;

        public const int attack = 48;
        public const int arrow = 49;
        public const int slash = 50;

        public const int capsule_gravity = 70;
        public const int capsule = 71;

        public const int skin = 80;
        public const int spectre = 81;

        public const int camera_shot = 89;
        public const int camera = 90;
    }

    public static class triheroes_res
    {
        public static readonly game_resources <CurveRes> curve = new game_resources<CurveRes> ( "Path" );
    }
}