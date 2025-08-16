using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public static class Hash
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
    }

    public static class Const
    {
        public static readonly Quaternion BowDefaultRotation = Quaternion.identity;
        public static readonly Quaternion SwordDefaultRotation = Quaternion.Euler(0, -90, 0);
    }

    // animation key library
    public static class AnimationKey
    {
        public static readonly term idle = new term("idle");
        public static readonly term walk = new term("walk");
        public static readonly term run = new term("run");
        public static readonly term run_lateral = new term("run_lateral");
        public static readonly term sprint = new term("sprint");
        public static readonly term sprint_brake = new term("sprint_brake");
        public static readonly term rotation_brake = new term("rotation_brake");
        public static readonly term fall = new term("fall");
        public static readonly term fall_end = new term("fall_end");
        public static readonly term fall_end_left_foot = new term("fall_end_left_foot");
        public static readonly term fall_end_right_foot = new term("fall_end_right_foot");
        public static readonly term fall_end_hard = new term("fall_end_hard");
        public static readonly term jump = new term("jump");
        public static readonly term jump_left_foot = new term("jump_left_foot");
        public static readonly term jump_right_foot = new term("jump_right_foot");

        public static readonly term take_sword = new term("take_sword");
        public static readonly term take_bow = new term("take_bow");
        public static readonly term return_bow = new term("return_bow");
        public static readonly term return_sword = new term("return_sword");

        public static readonly term dash_forward = new term("dash_forward");
        public static readonly term dash_back = new term("dash_back");
        public static readonly term dash_left = new term("dash_left");
        public static readonly term dash_right = new term("dash_right");

        public static readonly term perfect_dash = new term("perfect_dash");
        
        public static readonly term backflip = new term("backflip");

        public static readonly term slash_0 = new term("slash_0");
        public static readonly term slash_1 = new term("slash_1");
        public static readonly term slash_2 = new term("slash_2");
        public static readonly term parry_0 = new term("parry_0");
        public static readonly term parry_1 = new term("parry_1");
        public static readonly term parry_2 = new term("parry_2");

        public static readonly term SS7_0 = new term("SS7_0");
        public static readonly term SS7_1 = new term("SS7_1");
        public static readonly term SS7_2 = new term("SS7_2");
        
        public static readonly term begin_aim = new term("begin_aim");
        public static readonly term start_shoot = new term("start_shoot");
    }

    /// <summary>
    /// state key library for nodes
    /// </summary>
    public static class StateKey
    {
        public static readonly term zero = new term("zero");

        public static readonly term brake = new term("brake");
        public static readonly term brake_rotation = new term("brake_rotation");
        public static readonly term idle = new term("idle");
        public static readonly term walk = new term("walk");
        public static readonly term run_lateral = new term("run_lateral");
        public static readonly term walk_tired = new term("walk_tired");
        public static readonly term run = new term("run");
        public static readonly term sprint = new term("sprint");
    }

    // priorities
    public static class Pri
    {
        public static readonly int def = 0;
        public static readonly int def2nd = 1;
        public static readonly int Action = 2;
        public static readonly int Action2nd = 3;
        public static readonly int ForcedAction = 4;
        public static readonly int ForcedAction2nd = 5;
        public static readonly int Recovery = 6;

        public static readonly int SubAction = 2;
    }

    // AI commands
    public static class Commands
    {
        public static readonly term zero = new term("zero");

        public static readonly term draw_sword = new term ("draw_sword");
    }

    public enum direction { forward, right, left, back }
}
