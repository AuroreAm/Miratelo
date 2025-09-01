using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    [CreateAssetMenu(menuName = "Triheroes/Game")]
    public class Game : ScriptableObject
    {
        public CharacterAuthor[] ActivePartyMembers;
        public Vector3[] ActivePartyMembersPosition;
        public Vector3[] ActivePartyMembersRotation;
        public string MapId;
    }
}