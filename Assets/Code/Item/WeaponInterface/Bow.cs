using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class Bow : Weapon
    {
        public override WeaponType Type => WeaponType.Bow;
        public override term DefaultDrawAnimation => AnimationKey.take_bow;
        public override term DefaultReturnAnimation => AnimationKey.return_bow;


        public float Speed;
        
        // TODO: change this to hash
        public string ArrowName;
        public Transform BowString;
    }
}