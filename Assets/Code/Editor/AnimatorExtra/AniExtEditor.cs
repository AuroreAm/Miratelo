using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using Pixify;
using Triheroes.Code;
using System.IO;

namespace Triheroes.Editor
{

    public class AniExtProcessor : AssetPostprocessor
    {
        const string MetadataFolder = "Assets/Content/Resources/AnimatorMetadata";

        void OnPostprocessModel(GameObject gameObject)
        {
            if (!RefreshPending)
            {
                EditorApplication.delayCall += Refresh;
                RefreshPending = true;
            }
        }

        static bool RefreshPending;
        void Refresh ()
        {
            // get all metadata assets and recreate all metadata
            AniExt[] metadata = Resources.LoadAll<AniExt>("AnimatorMetadata");
            foreach (AniExt m in metadata)
            {
                if (m.Model != null)
                {
                    TransferMetadata(m.Model as AnimatorController, m);
                    EditorUtility.SetDirty(m);
                }
                else
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(m));
            }
            AssetDatabase.SaveAssets();
            RefreshPending = false;
        }

        static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string assetPath in importedAssets)
            if ( assetPath.EndsWith(".controller") )
            {
                // get the name of the controller
                string controllerName = Path.GetFileNameWithoutExtension(assetPath);
                AnimatorController controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(assetPath);

                // check for metadata folder path
                if (!AssetDatabase.IsValidFolder(MetadataFolder))
                {
                    Directory.CreateDirectory(MetadataFolder);
                    AssetDatabase.Refresh();
                }

                // get the file metadata target path
                string targetPath = MetadataFolder + "/" + controllerName + ".asset";

                // get the file
                AniExt target = AssetDatabase.LoadAssetAtPath<AniExt>(targetPath);

                // check for duplicate
                if (target != null && target.Model != null && target.Model != controller)
                {
                    Debug.LogError(controllerName + " " + "already exists");
                    return;
                }

                // Create the file if it doesn't exist
                if (target == null)
                {
                    target = ScriptableObject.CreateInstance<AniExt>();
                    AssetDatabase.CreateAsset(target, targetPath);
                }

                // transfer metadata
                TransferMetadata(controller, target);
                target.Model = controller;

                // save the file
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
            }
        }

        static void TransferMetadata ( AnimatorController from, AniExt target )
        {
            AnimatorControllerLayer[] layers = from.layers;
            List<bool> realLayer = new List<bool>();
            target.States = new Dictionary<term, AniExt.State>();

            for (int i = 0; i < layers.Length; i++)
            {
                if (layers[i].syncedLayerIndex == -1)
                {
                    WriteMetadataPerLayer(layers[i].stateMachine, ref target.States);
                    realLayer.Add(true);
                }
                else
                    realLayer.Add(false);
            }

            target.RealLayer = realLayer.ToArray();
            EditorUtility.SetDirty(target);


            void WriteMetadataPerLayer(AnimatorStateMachine stateMachine, ref Dictionary<term, AniExt.State> metaStates)
            {
                var states = stateMachine.states;
                foreach (var s in states)
                {
                    if (metaStates.ContainsKey(new term(s.state.name)))
                        continue;

                    if (s.state.motion is AnimationClip c)
                    {
                        float[] ef = new float[c.events.Length];

                        for (int i = 0; i < c.events.Length; i++)
                        {
                            ef[i] = c.events[i].time / s.state.speed;
                        }

                        metaStates.Add(new term(s.state.name),
                        new AniExt.State()
                        {
                            Key = new term(s.state.name),
                            Duration = c.isLooping ? Mathf.Infinity : c.length / s.state.speed,
                            EvPoint = ef
                        }
                        );
                    }
                    else
                        metaStates.Add(new term(s.state.name),
                        new AniExt.State()
                        {
                            Key = new term(s.state.name),
                            Duration = Mathf.Infinity
                        }
                        );
                }
            }
        }
    }
}