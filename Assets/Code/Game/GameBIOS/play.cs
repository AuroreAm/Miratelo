using System.Collections;
using System.Collections.Generic;
using Pixify;
using static Pixify.treeBuilder;
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
        action[] MainPlayerScripts;
        m_HP[] MainPlayerHP;
        m_ie[] MainPlayerIE;

        public static Character MainCharacter => o.MainActors[o.MainActor].character;
        public static Character GetMainCharacter(int i) => o.MainActors[i].character;
        public static int ActorCount => o.MainActors.Length;

        void SpawnMainCharacters()
        {
            MainActors = new m_actor[GameData.o.LoadedGame.ActivePartyMembers.Length];

            for (int i = 0; i < GameData.o.LoadedGame.ActivePartyMembers.Length; i++)
            {
                MainActors[i] = GameData.o.LoadedGame.ActivePartyMembers[i].Spawn(GameData.o.LoadedGame.ActivePartyMembersPosition[i], Quaternion.Euler(GameData.o.LoadedGame.ActivePartyMembersRotation[i])).RequireModule<m_actor>();
            }
        }

        protected override void OnAquire()
        {
            MainPlayerScripts = new action[MainActors.Length];
            MainPlayerHP = new m_HP[MainActors.Length];
            MainPlayerIE = new m_ie[MainActors.Length];

            for (int i = 0; i < MainPlayerScripts.Length; i++)
            {
                TreeStart(MainActors[i].character);
                MainPlayerScripts[i] = PlayerControllerLibrary.MainPlayer();

                MainPlayerHP[i] = MainActors[i].character.RequireModule<m_HP>();
                MainPlayerIE[i] = MainActors[i].character.RequireModule<m_ie>();
            }

            SetMainPlayer(MainActor);
        }

        void SetMainPlayer(int i)
        {
            MainActors[i].character.RequireModule<m_character_controller>().StartRoot(MainPlayerScripts[i]);
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
            PlayerInteraction();
        }

        void PlayerHUD()
        {
            // show stats to UI
            gf_player_hud.o.SetCurrentHP(MainPlayerHP[MainActor].HP);
            gf_player_hud.o.SetCurrentIE(MainPlayerIE[MainActor].IE);
        }

        Coroutine GetNearInteractableC;
        Collider[] InteractableColliders = new Collider[30];
        p_interactable currentInteractable;
        IEnumerator GetNearInteractable()
        {
            var y = new WaitForSeconds (.6f);
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

        void PlayerInteraction()
        {
            if ( currentInteractable is pi_weapon piw )
            {
                gf_interact.ShowInteractText( string.Concat ("take ", piw.weapon.Name) );
            }
        }
    }
}
