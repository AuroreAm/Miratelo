using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pixify.Spirit;
using System;

namespace Pixify.Editor
{
    [CustomEditor (typeof(ActionPaper),true)]
    public class ActionPaperEditor : UnityEditor.Editor
    {
        ActionPaper Target;
        action paper;
        Cursor <action> cursor;
        PixEditor pE;

        void OnEnable ()
        {
            Target = target as ActionPaper;

            if ( !string.IsNullOrEmpty(Target.paper.StrNodeType) && Type.GetType ( Target.paper.StrNodeType ) != null )
            {
                paper = (action) FormatterServices.GetUninitializedObject ( Type.GetType ( Target.paper.StrNodeType ) );
                JsonUtility.FromJsonOverwrite ( Target.paper.StrNodeData, paper );
                Target.gameObject.name = paper.GetType ().Name;
            }
        }

        [MenuItem ("GameObject/ActionPaper")]
        static void CreateActionPaper ()
        {
            var a = new GameObject ().AddComponent <Script> ();
            a.gameObject.name = "---";
            
            if (Selection.activeTransform)
            a.transform.SetParent (Selection.activeTransform);

            Selection.activeGameObject = a.gameObject;
        }

        public override void OnInspectorGUI()
        {
            PixSelectionGUI ();
            PixEditorGUI ();
            DecoratorExtraGUI ();
        }

        void PixSelectionGUI ()
        {
            if ( paper != null ) return;

            if ( cursor == null )
            cursor = new Cursor<action> ( SetPix );

            cursor.GUI ();

            void SetPix (Type t)
            {
                paper = (action) FormatterServices.GetUninitializedObject ( t );
                Target.gameObject.name = t.Name;
                
                Target.paper.StrNodeType = paper.GetType ().AssemblyQualifiedName;
                Target.paper.StrNodeData = JsonUtility.ToJson (paper);
            }
        }

        void PixEditorGUI ()
        {
            if (paper == null) return;

            if ( pE == null )
            pE = PixEditor.CreateEditor (paper);

            EditorGUI.BeginChangeCheck();
            pE.GUI ();
            if (EditorGUI.EndChangeCheck())
            {
                Target.paper.StrNodeType = paper.GetType ().AssemblyQualifiedName;
                Target.paper.StrNodeData = JsonUtility.ToJson (paper);
                EditorUtility.SetDirty ( Target );
            }

            GUILayout.Space (32);
            if (GUILayout.Button ("Break"))
            {
                Target.gameObject.name = "---";
                paper = null;
                Target.paper.StrNodeType = "";
                Target.paper.StrNodeData = "";
                EditorUtility.SetDirty ( Target );
            }
        }

        Cursor <action> ExtraCursor;
        void DecoratorExtraGUI ()
        {
            if (paper == null) return;
            if ( ! (paper is decorator) ) return;

            if (ExtraCursor == null) ExtraCursor = new Cursor<action> (AddPix);

            ExtraCursor.GUI ();

            void AddPix (Type t)
            {
                ActionPaper p = new GameObject (t.Name).AddComponent <ActionPaper> ();

                p.paper.StrNodeType = t.AssemblyQualifiedName;
                p.paper.StrNodeData = JsonUtility.ToJson (  FormatterServices.GetUninitializedObject ( t ) );

                p.transform.SetParent ( Target.transform );

                EditorUtility.SetDirty (p);
            }
        }
    }
}