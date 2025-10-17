using Lyra;
using System.Collections.Generic;
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
        public static readonly term force = new term ("force");
        public static readonly term player = new term ("player");
    }

    public static class anim
    {
        public static readonly term idle = new term("idle");
        public static readonly term walk = new term("walk");
        public static readonly term run = new term("run");
        public static readonly term run_lateral = new term("run_lateral");
        public static readonly term sprint = new term("sprint");
        public static readonly term brake = new term("brake");
        public static readonly term rotation_brake = new term("rotation_brake");
        public static readonly term fall = new term("fall");
        public static readonly term fall_end = new term("fall_end");
        public static readonly term fall_end_left_foot = new term("fall_end_left_foot");
        public static readonly term fall_end_right_foot = new term("fall_end_right_foot");
        public static readonly term fall_end_hard = new term("fall_end_hard");
        public static readonly term jump = new term("jump");
        public static readonly term jump_left_foot = new term("jump_left_foot");
        public static readonly term jump_right_foot = new term("jump_right_foot");

        public static readonly term hit_knocked_a = new term ("hit_knocked_a");
        public static readonly term hit_knocked_b = new term ("hit_knocked_b");
        public static readonly term stand_up = new term ("stand_up");

        public static readonly term take_sword = new term("take_sword");
        public static readonly term take_bow = new term("take_bow");
        public static readonly term return_bow = new term("return_bow");
        public static readonly term return_sword = new term("return_sword");

        public static readonly term dash_forward = new term("dash_forward");
        public static readonly term dash_back = new term("dash_back");
        public static readonly term dash_left = new term("dash_left");
        public static readonly term dash_right = new term("dash_right");

        public static readonly term stun_begin = new term("stun_begin");
        public static readonly term stun = new term("stun");
        
        public static readonly term backflip = new term("backflip");

        public static readonly term SS1_0 = new term("SS1_0");
        public static readonly term SS1_1 = new term("SS1_1");
        public static readonly term SS1_2 = new term("SS1_2");

        public static readonly term SS8_0 = new term("SS8_0");
        public static readonly term SS8_1 = new term("SS8_1");

        public static readonly term SS9_0 = new term("SS9_0");
        public static readonly term SS9_1 = new term("SS9_1");
        public static readonly term SS9_2 = new term("SS9_2");

        public static readonly term SS4 = new term("SS4");
        
        public static readonly term S1_charge = new term("S1_charge");
        public static readonly term S1 = new term ("S1");

        public static readonly term begin_aim = new term("begin_aim");
        public static readonly term start_shoot = new term("start_shoot");
    }

    [path ("skill")]
    public static class skl {
        public static readonly term SS1 = new term ( "SS1" );
    }

    public static class val
    {
        public static readonly Quaternion def_bow_rotation = Quaternion.identity;
        public static readonly Quaternion def_sword_rotation = Quaternion.Euler(0, -90, 0);
    }

    public static class GO {
        public static readonly term heart_hp = new term("heart_hp");
        public static readonly term heart_hp_glow = new term("heart_hp_glow");
        public static readonly term stamina_dot = new term ("stamina_dot");
        public static readonly term stamina_dot_glow = new term ("stamina_dot_glow");
        public static readonly term armor_wheel = new term ("armor_wheel");
        public static readonly term armor_wheel_glow = new term ("armor_wheel_glow");

        public static readonly term character_hud_label = new term("character_hud_label");
    }
}
