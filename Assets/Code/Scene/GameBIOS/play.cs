using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using System.IO;
using Pixify.Spirit;

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

        int MainPlayerIndex;
        block MainPlayer => MainHeroes [MainPlayerIndex];
        public d_actor MainActor => MainActors [MainPlayerIndex];

        block [] MainHeroes;
        d_actor [] MainActors;
        player_cortex [] PlayerControllers;

        d_HP [] MainPlayerHP;
        d_ie [] MainPlayerIE;

        void SpawnMainCharacters()
        {
            MainHeroes = new block [GameData.o.LoadedGame.ActivePartyMembers.Length];
            MainActors = new d_actor [GameData.o.LoadedGame.ActivePartyMembers.Length];
            PlayerControllers = new player_cortex [MainHeroes.Length];

            for (int i = 0; i < MainHeroes.Length; i++)
            {
                MainHeroes [i] = GameData.o.LoadedGame.ActivePartyMembers[i].Spawn(GameData.o.LoadedGame.ActivePartyMembersPosition[i], Quaternion.Euler(GameData.o.LoadedGame.ActivePartyMembersRotation[i]));
                MainActors [i] = MainHeroes [i].GetPix <d_actor> ();
                PlayerControllers [i] = new player_cortex ();
            }
        }

        protected override void Start()
        {
            MainPlayerHP = new d_HP[MainHeroes.Length];
            MainPlayerIE = new d_ie[MainHeroes.Length];

            for (int i = 0; i < MainPlayerHP.Length; i++)
            {
                MainPlayerHP[i] = MainHeroes [i].GetPix<d_HP>();
                MainPlayerIE[i] = MainHeroes [i].GetPix<d_ie>();
            }

            SetMainPlayer ( MainPlayerIndex );
        }

        void SetMainPlayer(int i)
        {
            MainPlayerIndex = i;
            MainPlayer.GetPix <s_mind> ().SetCortex ( PlayerControllers [i] );

            // set the player hud
            gf_player_hud.o.SetIdentity(MainPlayerHP[i].MaxHP, MainPlayerHP[i].HP, MainPlayerIE[i].MaxIE, MainPlayerIE[i].IE, MainActor.dd );
            // set the camera
            s_camera.o.TpsACharacter( MainPlayer.GetPix <d_actor> () );

            // start getting near interactable
            if ( GetNearInteractableC != null )
            Stage.o.StopCoroutine ( GetNearInteractableC );
            GetNearInteractableC = Stage.o.StartCoroutine(GetNearInteractable());
        }

        protected override void Step()
        {
            PlayerHUD();
        }

        void PlayerHUD()
        {
            // show stats to UI
            gf_player_hud.o.SetCurrentHP ( MainPlayerHP[MainPlayerIndex].HP );
            gf_player_hud.o.SetCurrentIE ( MainPlayerIE[MainPlayerIndex].IE );
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
                int q = Physics.OverlapSphereNonAlloc(MainActor.dd.position, distance, InteractableColliders, Vecteur.Trigger);
                currentInteractable = null;

                if (q > 0)
                {
                    // get the closest
                    Collider closest = null;
                    float CurrentDistance = distance;
                    for (int i = 0; i < q; i++)
                    {
                        if (Vector3.Distance(MainActor.dd.position, InteractableColliders[i].transform.position) < CurrentDistance)
                        {
                            CurrentDistance = Vector3.Distance(MainActor.dd.position, InteractableColliders[i].transform.position);
                            closest = InteractableColliders[i];
                        }
                    }

                    if (closest && Interactable.Contains(closest.id()))
                        currentInteractable = Interactable.GetInteractable(closest.id());
                }
                yield return y;
            }
        }
    }
}
