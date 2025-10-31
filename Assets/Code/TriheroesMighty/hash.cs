using Lyra;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Triheroes.Code
{
    public static class sh
    {
        //----- ANIMATION VAR ------
        public static readonly int spd = Animator.StringToHash("spd");
        public static readonly int lik = Animator.StringToHash("lik");
        public static readonly int rik = Animator.StringToHash("rik");
        public static readonly int dx = Animator.StringToHash("dx");
        public static readonly int dz = Animator.StringToHash("dz");
        public static readonly int x = Animator.StringToHash("x");
        public static readonly int y = Animator.StringToHash("y");
        public static readonly int z = Animator.StringToHash("z");

        //
        public static readonly term camera = new term ("camera");
        public static readonly term direct_capture = new term ("direct_capture");
        public static readonly term hud = new term ("hud");

        //
        public static readonly term force = new term ("force");
        public static readonly term player = new term ("player");
        public static readonly term after_image = new term ("after_image");

        public static readonly term decoherence_blink = new term ("decoherence_blink");
        public static readonly term red_blink = new term ("red_blink");
    }

    public static class sp {
        public static readonly term white = new term ( "white" );

        public static readonly term red_bloom = new term ("red_bloom");
        public static readonly term impact_metal = new term ("impact_metal");
        public static readonly term explosion = new term ( "explosion" );
    }

    public static class anim
    {
        static anim () {
            var fields = typeof (anim).GetFields ( BindingFlags.Public | BindingFlags.Static ).Where(f => f.FieldType == typeof(term) &&  f.IsInitOnly);

            foreach (var field in fields) {
                field.SetValue ( null, new term (field.Name) );
            }
        }

        public static readonly term idle;
        public static readonly term walk;
        public static readonly term run;
        public static readonly term run_lateral;
        public static readonly term sprint;
        public static readonly term brake;
        public static readonly term rotation_brake;
        public static readonly term fall;
        public static readonly term fall_end;
        public static readonly term fall_end_left_foot;
        public static readonly term fall_end_right_foot;
        public static readonly term fall_end_hard;
        public static readonly term jump;
        public static readonly term jump_left_foot;
        public static readonly term jump_right_foot;
        public static readonly term hit_knocked_a;
        public static readonly term hit_knocked_b;
        public static readonly term stand_up;

        public static readonly term take_sword;
        public static readonly term take_bow;
        public static readonly term return_bow;
        public static readonly term return_sword;

        public static readonly term dash_forward;
        public static readonly term dash_back;
        public static readonly term dash_left;
        public static readonly term dash_right;

        public static readonly term stun_begin;
        public static readonly term stun;
        
        public static readonly term backflip;
        public static readonly term SS1_0;
        public static readonly term SS1_1;
        public static readonly term SS1_2;
        public static readonly term SS8_0;
        public static readonly term SS8_1;

        public static readonly term SS9_0;
        public static readonly term SS9_1;
        public static readonly term SS9_2;

        public static readonly term SS4;
        
        public static readonly term S1_charge;
        public static readonly term S1;

        public static readonly term begin_aim;
        public static readonly term start_shoot;

        public static readonly term sleep;
        public static readonly term wake;
    }

    public static class val
    {
        public static readonly Quaternion def_bow_rotation = Quaternion.identity;
        public static readonly Quaternion def_sword_rotation = Quaternion.Euler(0, -90, 0);
    }

    public static class tl {
        public static readonly term heart = new term ("heart");
        public static readonly term stamina_dot = new term ("stamina_dot");
    }
}
