using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

namespace Lyra.Editor
{
    public class ScriptAuthorEditor : EditorWindow
    {
        [MenuItem ("GameObject/Script")]
        static void CreateActionPaper ()
        {
            var a = new GameObject ("---");
            a.AddComponent <ScriptAuthor> ();

            if (Selection.activeGameObject)
            a.transform.SetParent (Selection.activeGameObject.transform);

            Selection.activeGameObject = a;
            Undo.RegisterCreatedObjectUndo (a, "Create Script");
        }

        [MenuItem ("GameObject/Index")]
        static void CreateIndexPaper ()
        {
            var a = new GameObject ("---");
            a.AddComponent <IndexPaper> ();

            if (Selection.activeGameObject)
            a.transform.SetParent (Selection.activeGameObject.transform);

            Selection.activeGameObject = a;
            Undo.RegisterCreatedObjectUndo (a, "Create Index");
        }

        [MenuItem ("Window/Script Dock")]
        public static void ShowWindow ()
        {
            EditorWindow.GetWindow(typeof(ScriptAuthorEditor));
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

            if ( go && !( go.GetComponent <IndexPaper> () || go.GetComponent <ActionPaper> () ) )
            can = false;

            GUILayout.BeginHorizontal ();

            if (GUILayout.Button ("+",GUILayout.Width (64)) && can)
                ActionCursor.Show ( go );

            if ( GUILayout.Button ("New Index") )
                CreateIndexPaper ();

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

            if ( go.GetComponent <IndexPaper> () )
            EditorGUI.DrawRect ( selectionRect, new Color (.2f,.1f,.2f,.4f) );

            if ( go.GetComponent <ActionPaper> () )
            EditorGUI.DrawRect ( selectionRect, new Color (.4f,.4f,.6f,.4f) );

            if ( go.GetComponent <ActionPaper> () && go.GetComponent <ActionPaper> ().IsDecoratorKind () )
            EditorGUI.DrawRect ( selectionRect, new Color (.4f,.4f,.7f,.4f) );

            if ( go.GetComponent <ScriptAuthor> () )
            EditorGUI.DrawRect ( selectionRect, new Color (.7f,.4f,.4f,.4f) );
        }
    }
}
