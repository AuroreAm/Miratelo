using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "Slash", menuName = "RPG/SlashModel")]
    public class SlashAuthor : VirtusAuthor
    {
        protected override void RequiredPix(in List<Type> a)
        {
            a.A<a_slash_attack> ();
        }
    }
}
