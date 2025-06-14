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
    }

    public static class Const
    {
        public static readonly Quaternion BowDefaultRotation = Quaternion.identity;
        public static readonly Quaternion SwordDefaultRotation = Quaternion.Euler(0, -90, 0);
    }

    // animation key library
    public static class AnimationKey
    {
        public static readonly SuperKey idle = new SuperKey("idle");
        public static readonly SuperKey idle_tired = new SuperKey("idle_tired");
        public static readonly SuperKey idle_alt = new SuperKey("idle_alt");
        public static readonly SuperKey walk = new SuperKey("walk");
        public static readonly SuperKey run = new SuperKey("run");
        public static readonly SuperKey run_lateral = new SuperKey("run_lateral");
        public static readonly SuperKey sprint = new SuperKey("sprint");
        public static readonly SuperKey walk_tired = new SuperKey("walk_tired");
        public static readonly SuperKey sprint_brake = new SuperKey("sprint_brake");
        public static readonly SuperKey rotation_brake = new SuperKey("rotation_brake");
        public static readonly SuperKey fall = new SuperKey("fall");
        public static readonly SuperKey fall_end = new SuperKey("fall_end");
        public static readonly SuperKey fall_end_left_foot = new SuperKey("fall_end_left_foot");
        public static readonly SuperKey fall_end_right_foot = new SuperKey("fall_end_right_foot");
        public static readonly SuperKey jump = new SuperKey("jump");
        public static readonly SuperKey jump_left_foot = new SuperKey("jump_left_foot");
        public static readonly SuperKey jump_right_foot = new SuperKey("jump_right_foot");

        public static readonly SuperKey stand_up = new SuperKey ("stand_up");
        public static readonly SuperKey hit = new SuperKey("hit");
        public static readonly SuperKey hitu = new SuperKey ("hitu");
        public static readonly SuperKey hit_knocked_a = new SuperKey ("hit_knocked_a");
        public static readonly SuperKey hit_knocked_b = new SuperKey ("hit_knocked_b");

        public static readonly SuperKey take_sword = new SuperKey("take_sword");
        public static readonly SuperKey take_bow = new SuperKey("take_bow");
        public static readonly SuperKey return_bow = new SuperKey("return_bow");
        public static readonly SuperKey return_sword = new SuperKey("return_sword");

        public static readonly SuperKey dash_forward = new SuperKey("dash_forward");
        public static readonly SuperKey dash_back = new SuperKey("dash_back");
        public static readonly SuperKey dash_left = new SuperKey("dash_left");
        public static readonly SuperKey dash_right = new SuperKey("dash_right");

        public static readonly SuperKey slash_0 = new SuperKey("slash_0");
        public static readonly SuperKey slash_1 = new SuperKey("slash_1");
        public static readonly SuperKey slash_2 = new SuperKey("slash_2");
        public static readonly SuperKey parry_0 = new SuperKey("parry_0");
        public static readonly SuperKey parry_1 = new SuperKey("parry_1");
        public static readonly SuperKey parry_2 = new SuperKey("parry_2");
        
        public static readonly SuperKey slash_3a = new SuperKey("slash_3a");
        public static readonly SuperKey slash_3b = new SuperKey("slash_3b");
        public static readonly SuperKey begin_aim = new SuperKey("begin_aim");
        public static readonly SuperKey start_shoot = new SuperKey("start_shoot");
    }

    /// <summary>
    /// state key library for nodes
    /// </summary>
    public static class StateKey
    {
        public static readonly SuperKey zero = new SuperKey("zero");

        public static readonly SuperKey brake = new SuperKey("brake");
        public static readonly SuperKey brake_rotation = new SuperKey("brake_rotation");
        public static readonly SuperKey idle = new SuperKey("idle");
        public static readonly SuperKey walk = new SuperKey("walk");
        public static readonly SuperKey run_lateral = new SuperKey("run_lateral");
        public static readonly SuperKey walk_tired = new SuperKey("walk_tired");
        public static readonly SuperKey run = new SuperKey("run");
        public static readonly SuperKey sprint = new SuperKey("sprint");
        
        public static readonly SuperKey jump = new SuperKey("jump");

        public static readonly SuperKey slash = new SuperKey("slash");
        public static readonly SuperKey slash_jump = new SuperKey("slash_jump");
        public static readonly SuperKey parry_trajectile = new SuperKey("parry_trajectile");
        public static readonly SuperKey parry = new SuperKey("parry");

        public static readonly SuperKey aim = new SuperKey("aim");
    }

    /// <summary>
    /// state key library for decorators
    /// </summary>
    public static class StateKey2
    {
        public static readonly SuperKey zero = new SuperKey("zero");
        public static readonly SuperKey move = new SuperKey("move");
        public static readonly SuperKey fall = new SuperKey("fall");
        public static readonly SuperKey jump = new SuperKey("jump");
        public static readonly SuperKey draw = new SuperKey("draw");     
        public static readonly SuperKey return_ = new SuperKey("return");

        public static readonly SuperKey targetting =  new SuperKey ("targetting");
        
        public static readonly SuperKey msu = new SuperKey ("msu");
        public static readonly SuperKey slash = new SuperKey("slash");  

        public static readonly SuperKey mbu = new SuperKey ("mbu");
        public static readonly SuperKey aim = new SuperKey("aim");
    }

    public enum direction { forward, right, left, back }
}
