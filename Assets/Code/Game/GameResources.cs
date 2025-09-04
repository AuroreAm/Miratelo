using System.Collections;
using System.Collections.Generic;
using Lyra;

namespace Triheroes.Code
{

    public static class SysOrder
    {
        public const int s_ground_data_ccc = -70;

        public const int s_gravity_ccc = 70;
        public const int s_capsule_character_controller = 71;

        public const int s_skin = 80;

        public const int camera_shot = 89;
        public const int s_camera = 90;
    }

    public static class TriheroesRes
    {
        public static readonly book <CurveRes> Curve = new book<CurveRes> ( "Path" );
    }
}