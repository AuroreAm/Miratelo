using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace Lyra.Editor
{
    public class ScriptAuthorEditor : EditorWindow
    {
        [MenuItem ("GameObject/ScriptAuthor")]
        static void CreateScriptAuthor ()
        {
            var a = new GameObject ("---");
            a.AddComponent <ScriptAuthor> ();

            if (Selection.activeGameObject)
            a.transform.SetParent (Selection.activeGameObject.transform);

            Selection.activeGameObject = a;
            Undo.RegisterCreatedObjectUndo (a, "Create Script");
        }

        [MenuItem ("GameObject/Action")]
        static void CreateActionPaper() {
            var go = Selection.activeGameObject;
            ActionCursor.Show ( go );
        }

        [MenuItem ("Window/Lyra/Script Dock")]
        public static void ShowWindow ()
        {
            GetWindow(typeof(ScriptAuthorEditor));
        }

        void OnEnable ()
        {
            minSize = new Vector2 (0, 32);
            titleContent = new GUIContent ("Script");
        }

        void OnGUI ()
        {
            var go = Selection.activeGameObject;

            bool can = true;

            if (!go)
            can = false;

            if ( go && !( go.GetComponent<ScriptAuthor> () || go.GetComponent <ActionPaper> ()) )
            can = false;

            GUILayout.BeginHorizontal ();

            if (GUILayout.Button ("+",GUILayout.Width (64)) && can)
                ActionCursor.Show ( go );

            if (GUILayout.Button ("-",GUILayout.Width (32)) && can)
                Undo.DestroyObjectImmediate ( go );

            GUILayout.EndHorizontal ();
        }
    }

    [InitializeOnLoad]
    class ScriptHierarchyColors
    {
        static ScriptHierarchyColors ()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        static void OnHierarchyWindowItemOnGUI ( int instanceID, Rect selectionRect )
        {
            var go = EditorUtility.InstanceIDToObject ( instanceID ) as GameObject;

            if ( go == null ) return;

            ActionPaper actionPaper = go.GetComponent <ActionPaper> ();
            if ( actionPaper )
            {
                EditorGUI.DrawRect ( selectionRect, new Color (.1f,.1f,.4f) );
                if (  actionPaper.IsDecoratorKind () )
                EditorGUI.DrawRect ( selectionRect, new Color (.4f,.2f,.1f) );

                string Label = "---";
                Type t = actionPaper.GetPaperType ();
                if ( t != null )
                Label =  t.Name;
                Label += $"({actionPaper.gameObject.name})";

                EditorGUI.LabelField ( selectionRect, Label, Styles.o.Action );
            }

            if ( go.GetComponent <ScriptAuthor> () )
            EditorGUI.DrawRect ( selectionRect, new Color (.7f,.4f,.4f,.4f) );
        }
    }
}
