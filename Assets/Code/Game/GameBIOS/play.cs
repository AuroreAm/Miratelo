using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using System.IO;

namespace Triheroes.Code
{
    // bios for the main gameplay
    // host for character in scene
    // play a character and let AI move the others in party
    // switch character
    public class play : bios
    {
        public static play o;

        public override void Create()
        {
            o = this;
            SpawnMainCharacters();
        }

        m_actor[] MainActors;
        int MainActor;
        m_HP[] MainPlayerHP;
        m_ie[] MainPlayerIE;
        player_cortex [] PlayerCortexes;

        public static Character MainCharacter => o.MainActors[o.MainActor].character;
        public static Character GetMainCharacter(int i) => o.MainActors[i].character;
        public static int ActorCount => o.MainActors.Length;

        void SpawnMainCharacters()
        {
            MainActors = new m_actor[GameData.o.LoadedGame.ActivePartyMembers.Length];
            PlayerCortexes = new player_cortex [GameData.o.LoadedGame.ActivePartyMembers.Length];

            for (int i = 0; i < GameData.o.LoadedGame.ActivePartyMembers.Length; i++)
            {
                MainActors[i] = GameData.o.LoadedGame.ActivePartyMembers[i].Spawn(GameData.o.LoadedGame.ActivePartyMembersPosition[i], Quaternion.Euler(GameData.o.LoadedGame.ActivePartyMembersRotation[i])).RequireModule<m_actor>();

                PlayerCortexes [i] = New <player_cortex> ( GetMainCharacter (i) );
            }
        }

        protected override void OnAquire()
        {
            MainPlayerHP = new m_HP[MainActors.Length];
            MainPlayerIE = new m_ie[MainActors.Length];

            for (int i = 0; i < MainPlayerHP.Length; i++)
            {
                MainPlayerHP[i] = MainActors[i].character.RequireModule<m_HP>();
                MainPlayerIE[i] = MainActors[i].character.RequireModule<m_ie>();
            }

            SetMainPlayer(MainActor);
        }

        void SetMainPlayer(int i)
        {
            MainCharacter.RequireModule <m_cortex> ().SetCortex ( PlayerCortexes [i] );

            // set the player hud
            gf_player_hud.o.SetIdentity(MainPlayerHP[i].MaxHP, MainPlayerHP[i].HP, MainPlayerIE[i].MaxIE, MainPlayerIE[i].IE, MainActors[i].md);
            // set the camera
            m_camera.o.TpsACharacter(MainActors[i]);

            // start getting near interactable
            if ( GetNearInteractableC != null )
            character.StopCoroutine ( GetNearInteractableC );
            GetNearInteractableC = character.StartCoroutine(GetNearInteractable());

            MainActor = i;
        }

        public override void Main()
        {
            PlayerHUD();
        }

        void PlayerHUD()
        {
            // show stats to UI
            gf_player_hud.o.SetCurrentHP(MainPlayerHP[MainActor].HP);
            gf_player_hud.o.SetCurrentIE(MainPlayerIE[MainActor].IE);
        }

        Coroutine GetNearInteractableC;
        Collider[] InteractableColliders = new Collider[30];
        public p_interactable currentInteractable { get; private set; }
        IEnumerator GetNearInteractable()
        {
            var y = new WaitForSeconds (.3f);
            while (true)
            {
                // get near interactable object
                const float distance = 2;
                int q = Physics.OverlapSphereNonAlloc(MainCharacter.transform.position, distance, InteractableColliders, Vecteur.Trigger);
                currentInteractable = null;

                if (q > 0)
                {
                    // get the closest
                    Collider closest = null;
                    float CurrentDistance = distance;
                    for (int i = 0; i < q; i++)
                    {
                        if (Vector3.Distance(MainCharacter.transform.position, InteractableColliders[i].transform.position) < CurrentDistance)
                        {
                            CurrentDistance = Vector3.Distance(MainCharacter.transform.position, InteractableColliders[i].transform.position);
                            closest = InteractableColliders[i];
                        }
                    }

                    if (closest && Interactable.ThingExist(closest.id()))
                        currentInteractable = Interactable.GetInteractable(closest.id());
                }
                yield return y;
            }
        }
    }
}
