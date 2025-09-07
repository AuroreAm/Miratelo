using System.Collections.Generic;
using Lyra;
using Triheroes.Code;
using UnityEditor;
using UnityEngine;
using UnityEditor.Animations;
using System;
using static Triheroes.Code.Sword.Combat.slay;

namespace Triheroes.Editor
{
    [CustomEditor(typeof(SlashSkinPathAuthor))]
    public class SlashSkinPathAuthorEditor : UnityEditor.Editor
    {
        readonly term[] EditableClip = new term[] {
            animation.SS1_0,animation.SS1_1,animation.SS1_2,
            animation.SS4
         };

        SlashSkinPathAuthor Target;
        Animator Ani;
        Transform TargetHand;
        Transform HandEnd;
        Transform CustomDirection;

        void OnEnable()
        {
            Target = ( SlashSkinPathAuthor ) target;
            Ani = Target.GetComponent<Animator> ();
        }

        public override void OnInspectorGUI()
        {
            if ( String.IsNullOrEmpty (Target.gameObject.scene.name) )
            {
                GUILayout.Label ( "Must Be In Scene" );
                return;
            }
            base.OnInspectorGUI ();
            HandSelectionGUI ();

            if ( !TargetHand || !Ani )
                return;

            ClipSelectionGUI ();
        }

        void HandSelectionGUI ()
        {
            GUILayout.Label ( "Select Hand" );
            TargetHand = EditorGUILayout.ObjectField ( "Target Hand", TargetHand, typeof(Transform), true ) as Transform;

            GUILayout.BeginHorizontal ();
            GUILayout.Label ("Or");
            if (GUILayout.Button ("Left Hand"))
                TargetHand = Ani.GetComponent <CapsuleSkinAuthor> ().Hand [0];
            if (GUILayout.Button ("Right Hand"))
                TargetHand = Ani.GetComponent <CapsuleSkinAuthor> ().Hand [1];
            GUILayout.EndHorizontal ();

            CustomDirection = EditorGUILayout.ObjectField ( "Custom Direction", CustomDirection, typeof(Transform), true ) as Transform;
        }

        void ClipSelectionGUI ()
        {
            GUILayout.Label ( "Sample a Clip" );
            foreach (var k in EditableClip)
                if ( GUILayout.Button (k.name) )
                {
                    var c = FindClipInAnimator ( k );
                    if (c != null)
                        SampleClip ( c, k );
                }
        }

        void SampleClip ( AnimationClip c, term term )
        {
            AnimationEvent[] EventSegment;
            if (!CheckIfValidClipAndGetSegment ( c, out EventSegment ))
                return;

            Vector3 PositionCache = Target.transform.position;
            Quaternion RotationCache = Target.transform.rotation;
            Target.transform.position = Vector3.zero;
            Target.transform.rotation = Quaternion.identity;
            SetHand ();
            AnimationMode.StartAnimationMode ();

            // collect sample times
            float startTime = EventSegment[0].time;
            float endTime   = EventSegment[1].time;

            List<float> sampleTimes = new List<float>();
            for (float t = startTime; t < endTime; t += Code.Sword.Combat.slay.path.delta)
                sampleTimes.Add(t);
            sampleTimes.Add(endTime);

            // sample
            path path = new path ();
            path.key = term;
            path.orig = new Vector3 [sampleTimes.Count];
            path.dir = new Vector3 [sampleTimes.Count];

            for (int i = 0; i < sampleTimes.Count; i++)
            {
                AnimationMode.SampleAnimationClip ( Target.gameObject, c, sampleTimes[i] );
                path.orig[i] = TargetHand.position;
                path.dir[i] = HandEnd.position - TargetHand.position;
            }

            // add sample
            Target.Paths.AddOrChange ( term, path );
            EditorUtility.SetDirty ( Target );

            AnimationMode.EndSampling();
            AnimationMode.StopAnimationMode();
            Target.transform.position = PositionCache;
            Target.transform.rotation = RotationCache;
            ResetHand ();
            SceneView.RepaintAll();

            Debug.Log ( "Done Sampling" );
        }

        bool CheckIfValidClipAndGetSegment ( AnimationClip c, out AnimationEvent[] events )
        {
            events = null;
            var Evs = c.events;
            List<AnimationEvent> Result = new List<AnimationEvent> ();
            foreach ( var e in Evs )
            {
                if ( e.functionName == "F")
                {
                    Result.Add ( e );
                }
            }
            if (Result.Count == 2)
            {
                events = Result.ToArray ();
                return true;
            }
            return false;
        }

        void SetHand ()
        {
            HandEnd = new GameObject ("HandEnd").transform;
            HandEnd.parent = TargetHand;
            HandEnd.localPosition = Vector3.zero;

            if (!CustomDirection)
            HandEnd.localRotation = Const.SwordDefaultRotation;
            else
            HandEnd.rotation = Quaternion.LookRotation ( CustomDirection.position - TargetHand.position );

            HandEnd.position += HandEnd.TransformDirection ( Vector3.forward );
        }

        void ResetHand()
        {
            DestroyImmediate ( HandEnd.gameObject );
        }

        AnimationClip FindClipInAnimator ( term k )
        {
            AnimatorControllerLayer[] layers = ((AnimatorController)Ani.runtimeAnimatorController).layers;
            List<ChildAnimatorState> States = new List<ChildAnimatorState>();

            foreach (var l in layers)
                States.AddRange(l.stateMachine.states);

            return States.Find(x => x.state.motion is AnimationClip && string.Equals(x.state.name, k.name)).state.motion as AnimationClip;
        }
    }
}