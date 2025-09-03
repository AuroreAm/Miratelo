using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // priorities
    public static class Rank
    {
        public static readonly int Def = 0;
        public static readonly int Def2nd = 1;
        public static readonly int Def3rd = 2;
        public static readonly int Action = 3;
        public static readonly int Action2nd = 4;
        public static readonly int ForcedAction = 5;
        public static readonly int Recovery = 6;

        public static readonly int SubAction = 2;
    }

    public static class Const
    {
        public static readonly Quaternion BowDefaultRotation = Quaternion.identity;
        public static readonly Quaternion SwordDefaultRotation = Quaternion.Euler(0, -90, 0);
    }
}
