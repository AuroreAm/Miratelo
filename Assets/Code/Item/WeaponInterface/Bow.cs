using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class Bow : Weapon
    {
        public override WeaponType Type => WeaponType.Bow;
        public override SuperKey DefaultDrawAnimation => AnimationKey.take_bow;
        public override SuperKey DefaultReturnAnimation => AnimationKey.return_bow;

        public Transform BowString;
    }
}