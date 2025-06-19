using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "Slash", menuName = "RPG/SlashModel")]
    public class SlashAuthor : UnitAuthor
    {
        protected override void OnInstance(Unit newUnit)
        {
            newUnit.RequirePiece<p_slash_attack>();
        }
    }
}
